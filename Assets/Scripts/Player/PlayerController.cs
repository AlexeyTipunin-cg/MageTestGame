using UnityEngine;
using Assets.Scripts.Scene;
using Zenject;
using UniRx;
namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CharacterAnimationController _animation;
        [SerializeField] private PlayerInputController _playerInputController;
        [SerializeField] private WallDetection _wallDetection;

        private PlayerStateMachine _playerStateMachine;
        private CreatureConfig _wizardConfig;
        private Camera _camera;
        private Vector3 _cameraCharacterDelta;

        private ISceneLimits _sceneLimits;

        private bool _collidedWithWall;

        public Rigidbody RigidBody => _rigidbody;
        public PlayerInputController Input => _playerInputController;
        public WallDetection WallDetection => _wallDetection; 
        public CreatureConfig WizardConfig => _wizardConfig;
        public ISceneLimits SceneLimits => _sceneLimits;
        public Camera Camera => _camera;


        [Inject]
        private void Init(Camera cameraInjected, LevelConfig levelConfig, ISceneLimits sceneLimits, PlayerModel playerModel)
        {
            _camera = cameraInjected;
            _cameraCharacterDelta = gameObject.transform.position + _camera.transform.position;
            _wizardConfig = levelConfig.playerConfig;
            _sceneLimits = sceneLimits;

            _playerStateMachine = new PlayerStateMachine(this, _animation);

            playerModel.isDead.Subscribe(isDead =>
            {
                _playerStateMachine.ChangeState(_playerStateMachine.DeadState);
            });

            _playerStateMachine.ChangeState(_playerStateMachine.IdleState);
        }

        private void Update()
        {
            _playerStateMachine?.Update();
        }


        private void FixedUpdate()
        {
            _playerStateMachine?.FixedUpdate();
        }

        public void UpdateCameraPosition()
        {
            Transform cameraTransform = _camera.transform;
            Vector3 playerPosition = _rigidbody.position;
            Vector3 newCameraPosition = new Vector3(playerPosition.x + _cameraCharacterDelta.x, cameraTransform.position.y,
                playerPosition.z + _cameraCharacterDelta.z);
            cameraTransform.position = newCameraPosition;
        }
    }
}