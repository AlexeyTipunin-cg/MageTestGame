using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UniRx.Triggers;
using Unity.VisualScripting;

namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour
    {
        private CharacterWizardController _character;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private int _protection;

        [SerializeField] private HealthBar _healthBar;

        private Vector3 _rotation;
        private Vector3 _direction;
        private bool _collidedWithPlayer;
        private EnemyModel _model;
        private PlayerModel _playerModel;

        public void Init(CharacterWizardController characterController, EnemyModel model, PlayerModel playerModel)
        {
            _model = model;
            _playerModel = playerModel;
            _character = characterController;
            
            _healthBar.Init(Camera.main);
            _healthBar.SetProgress(_model.health.Value, _model.MaxHealth );
            this.OnCollisionEnterAsObservable().Where(c => c.gameObject.layer == (int)GameLayers.Player).Subscribe(_=>
            {
                _collidedWithPlayer = true;
                StartCoroutine(DamagePlayer());
            }).AddTo(this);
            this.OnCollisionExitAsObservable().Where(c => c.gameObject.layer == 6).Subscribe(_ =>
            {
                _collidedWithPlayer = false;
                StopAllCoroutines();
            }).AddTo(this);

        }

        private IEnumerator DamagePlayer()
        {
            while (_collidedWithPlayer)
            {
                _playerModel.health.Value -= 5;
                yield return new WaitForSeconds(1);
            }
        }
        
        private void Update()
        {
            Vector3 targetPosition = _character.transform.position;
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

        private void OnParticleCollision(GameObject other)
        {
            DamageComponent damage = other.GetComponent<DamageComponent>();
            if (damage != null)
            {
                _model.health.Value -= damage.GetDamage() * 1;
                _healthBar.SetProgress(_model.health.Value, _model.MaxHealth );
                if (_model.IsDead())
                {
                    Destroy(gameObject);
                }
            }

        }
    }
}