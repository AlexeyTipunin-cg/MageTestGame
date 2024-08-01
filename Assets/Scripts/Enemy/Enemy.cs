using System.Collections;
using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using DefaultNamespace;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyTypes _enemyType;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Damageable _damageable;

        public CompositeDisposable disposable;

        private IGetPosition _playerPosition;
        private Vector3 _rotation;
        private Vector3 _direction;
        private bool _collidedWithPlayer;
        private EnemyModel _model;
        private PlayerModel _playerModel;
        private bool _isGameOver;

        public EnemyTypes EnemyType => _enemyType;

        public void Init(IGetPosition playerPosition, EnemyModel model, PlayerModel playerModel)
        {
            disposable = new CompositeDisposable();

            _model = model;
            _playerModel = playerModel;
            _playerPosition = playerPosition;
            _collidedWithPlayer = false;

            _playerModel.isDead.Subscribe(isDead =>
            {

                if (isDead)
                {
                    _isGameOver = isDead;
                    _rigidbody.isKinematic = true;
                }

            }).AddTo(disposable);

            _healthBar.Init(Camera.main);
            _healthBar.SetProgress(_model.health.Value, _model.MaxHealth);

            _model.health.Subscribe(health =>
            {
                _healthBar.SetProgress(health, _model.MaxHealth);
            }).AddTo(disposable);

            _damageable.Init(_model);

            this.OnCollisionEnterAsObservable().Where(c => c.gameObject.layer == (int)GameLayers.Player).Subscribe(_ =>
            {
                _collidedWithPlayer = true;
                StartCoroutine(DamagePlayer());
            }).AddTo(disposable);

            this.OnCollisionExitAsObservable().Where(c => c.gameObject.layer == (int)GameLayers.Player).Subscribe(_ =>
            {
                _collidedWithPlayer = false;
                StopAllCoroutines();
            }).AddTo(disposable);

            Observable.EveryUpdate().Subscribe(_ => UpdateEnemy()).AddTo(disposable);
            Observable.EveryUpdate().Subscribe(_ => FixedUpdateEnemy()).AddTo(disposable);

        }

        private IEnumerator DamagePlayer()
        {
            while (_collidedWithPlayer)
            {
                if (_isGameOver)
                {
                    break;
                }

                _playerModel.AddDamage(_model.Config.damage);
                yield return new WaitForSeconds(_model.Config.damageInterval);
            }
        }

        private void UpdateEnemy()
        {
            if (_isGameOver)
            {
                return;
            }

            Vector3 targetPosition = _playerPosition.GetPosition();
            _direction = targetPosition - transform.position;
            _rotation = Vector3.RotateTowards(transform.forward, _direction, 1000, 1000);
        }

        private void FixedUpdateEnemy()
        {
            if (_isGameOver)
            {
                return;
            }

            if (_collidedWithPlayer)
            {
                return;
            }
            _rigidbody.rotation = Quaternion.Euler(new Vector3(0, _rotation.y * _model.Config.rotationSpeed * Time.fixedDeltaTime, 0));
            _rigidbody.velocity = _direction.normalized * _model.Config.movementSpeed;
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            disposable.Dispose();
            disposable.Clear();
            StopAllCoroutines();
        }
    }
}