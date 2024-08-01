using UnityEngine;
using Assets.Scripts.Scene;
using DefaultNamespace;
using Zenject;
using UniRx;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CharacterAnimationController _animation;

        private CreatureConfig _wizardConfig;
        private Camera _camera;
        private Vector3 _moveDirection;
        private Vector3 _rotationDirection;
        private Vector3 _cameraCharacterDelta;

        private ISceneLimits _sceneLimits;
        private bool _isGameOver;

        private float _verticalDirection;

        [Inject]
        private void Init(Camera cameraInjected, LevelConfig levelConfig, ISceneLimits sceneLimits, PlayerModel playerModel)
        {
            _camera = cameraInjected;
            _cameraCharacterDelta = gameObject.transform.position + _camera.transform.position;
            _wizardConfig = levelConfig.playerConfig;
            _sceneLimits = sceneLimits;

            _animation.Init(playerModel);
            playerModel.isDead.Subscribe(isDead =>
            {
                _isGameOver = isDead;
            });
        }
        private void Update()
        {
            if (_isGameOver) { return; }

            _verticalDirection = Input.GetAxis("Vertical");
            _moveDirection = Vector3.forward * _verticalDirection;
            _rotationDirection = Vector3.right * Input.GetAxis("Horizontal");

        }

        private void FixedUpdate()
        {
            if (_isGameOver) { return; }


            Quaternion rotation = _rigidbody.rotation;
            if (!Mathf.Approximately(_rotationDirection.x, 0))
            {
                int direction = _rotationDirection.x > 0 ? 1 : -1;

                rotation *= Quaternion.Euler(Vector3.up * direction * _wizardConfig.rotationSpeed * Time.fixedDeltaTime);
                _rigidbody.MoveRotation(rotation);
            }

            Vector3 position = _rigidbody.position;
            bool wall = Physics.Raycast(position + Vector3.up * 0.5f, rotation * _moveDirection,
                out RaycastHit hit, 1.5f, 1 << (int)GameLayers.Walls);


            Vector3 newPosition = position + rotation * _moveDirection.normalized * (_wizardConfig.movementSpeed * Time.fixedDeltaTime);
            bool insideScene = _sceneLimits.isInsideScene(newPosition);
            if (insideScene)
            {
                if (!wall)
                {
                    if (!Mathf.Approximately(_verticalDirection, 0))
                    {
                        _rigidbody.MovePosition(newPosition);
                        _animation.RunAnimation();
                    }
                    else
                    {
                        _animation.IdleAnimation();
                    }

                }
            }
        }

        private void LateUpdate()
        {
            if (_isGameOver) { return; }

            var transform1 = _camera.transform;
            var position = _rigidbody.position;
            var pos = new Vector3(position.x + _cameraCharacterDelta.x, transform1.position.y,
                position.z + _cameraCharacterDelta.z);
            transform1.position = pos;
        }
    }
}