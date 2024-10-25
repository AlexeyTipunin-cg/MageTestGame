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
        [SerializeField] private WallDetection _wallDetection;

        private PlayerStateMachine _playerStateMachine;
        private CreatureConfig _wizardConfig;
        private PlayerInputController _playerInputController;
        private PlayerModel _playerModel;
        private ISceneLimits _sceneLimits;

        private bool _collidedWithWall;
        private ICameraProvider _cameraProvider;

        public Rigidbody RigidBody => _rigidbody;
        public PlayerInputController Input => _playerInputController;
        public WallDetection WallDetection => _wallDetection; 
        public CreatureConfig WizardConfig => _wizardConfig;

        public ISceneLimits SceneLimits => _sceneLimits;

        [Inject]
        private void Init(PlayerInputController playerInputController, ICameraProvider cameraProvider)
        {
            _playerInputController = playerInputController;
            _cameraProvider = cameraProvider;
        }
        public void Launch(PlayerModel playerModel, LevelConfig levelConfig, ISceneLimits sceneLimits)
        {

            _wizardConfig = levelConfig.playerConfig;
            _sceneLimits = sceneLimits;

            _playerStateMachine = new PlayerStateMachine(this, _animation);

            playerModel.isDead.Subscribe(isDead =>
            {
                if (isDead)
                {
                    _playerStateMachine.ChangeState(_playerStateMachine.DeadState);
                }
            }).AddTo(this);

            _playerStateMachine.ChangeState(_playerStateMachine.IdleState);

            _cameraProvider.GetCamera().OnHeroCreated(gameObject.transform);
        }

        private void Update()
        {
            _playerStateMachine?.Update();
        }


        private void FixedUpdate()
        {
            _playerStateMachine?.FixedUpdate();
        }
    }
}