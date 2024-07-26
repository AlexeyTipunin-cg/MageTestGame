using System;
using UniRx;
using UnityEngine;
using UniRx.Triggers;
namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour
    {
        private CharacterController _character;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        private Vector3 _rotation;
        private Vector3 _direction;
        private bool _collided;

        public void Init(CharacterController characterController)
        {
            _character = characterController;
            // this.OnCollisionEnterAsObservable().Where(c => c.gameObject.layer == 6).Subscribe(_=>
            // {
            //     _collided = true;
            //     _rigidbody.velocity = Vector3.zero;
            //     _rigidbody.isKinematic = true;
            // }).AddTo(this);
            // this.OnCollisionExitAsObservable().Where(c => c.gameObject.layer == 6).Subscribe(_ =>
            // {
            //     _collided = false;
            //     _rigidbody.isKinematic = false;
            // }).AddTo(this);

        }
        
        private void Update()
        {
            Vector3 targetPosition = _character.transform.position;
            _direction = targetPosition - transform.position;
            _rotation = Vector3.RotateTowards(transform.forward, _direction, 1000, 1000);
        }

        private void FixedUpdate()
        {
            if (_collided)
            {
                return;
            }
            _rigidbody.rotation = Quaternion.Euler(_rotation);
            _rigidbody.velocity = _direction * _speed * Time.fixedDeltaTime;
        }

        private void OnParticleCollision(GameObject other)
        {
            Destroy(gameObject);
            Debug.Log("particleCOlidded");
        }
    }
}