using System.Collections;
using Assets.Scripts.Player;
using DefaultNamespace;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private int _protection;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Damageable _damageable;

        public CompositeDisposable disposable;

        private IGetPosition _playerPosition;
        private Vector3 _rotation;
        private Vector3 _direction;
        private bool _collidedWithPlayer;
        private EnemyModel _model;
        private PlayerModel _playerModel;

        public void Init( IGetPosition playerPosition, EnemyModel model, PlayerModel playerModel)
        {
            disposable = new CompositeDisposable();

            _model = model;
            _playerModel = playerModel;
            _playerPosition = playerPosition;
            _collidedWithPlayer = false;

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

        }

        private IEnumerator DamagePlayer()
        {
            while (_collidedWithPlayer)
            {
                _playerModel.SetHealth(-5);
                yield return new WaitForSeconds(1);
            }
        }

        private void Update()
        {
            Vector3 targetPosition = _playerPosition.GetPosition();
            _direction = targetPosition - transform.position;
            _rotation = Vector3.RotateTowards(transform.forward, _direction, 1000, 1000);
        }

        private void FixedUpdate()
        {
            if (_collidedWithPlayer)
            {
                return;
            }
            _rigidbody.rotation = Quaternion.Euler(_rotation);
            _rigidbody.velocity = _direction * _speed * Time.fixedDeltaTime;
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