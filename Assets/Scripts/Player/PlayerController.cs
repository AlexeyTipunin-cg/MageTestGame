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

        private ISceneLimits _sceneLimits;

        private bool _collidedWithWall;

        public Rigidbody RigidBody => _rigidbody;
        public PlayerInputController Input => _playerInputController;
        public WallDetection WallDetection => _wallDetection; 
        public CreatureConfig WizardConfig => _wizardConfig;
        public ISceneLimits SceneLimits => _sceneLimits;


        [Inject]
        private void Init(LevelConfig levelConfig, ISceneLimits sceneLimits, PlayerModel playerModel)
        {
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
    }
}