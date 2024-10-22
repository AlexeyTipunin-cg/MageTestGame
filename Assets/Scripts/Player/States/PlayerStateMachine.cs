namespace Assets.Scripts.Player
{
    public class PlayerStateMachine
    {
        public IdleState IdleState { get; }
        public MoveState MoveState { get; }
        public DeadState DeadState { get; }

        private BaseState _currentState;

        private CharacterAnimationController _animationController;
        private PlayerController _playerController;
        public CharacterAnimationController AnimtaionController => _animationController;
        public PlayerController Player => _playerController;

        public PlayerStateMachine(PlayerController player, CharacterAnimationController animationController)
        {
            _playerController = player;
            _animationController = animationController;
            IdleState = new IdleState(this);
            MoveState = new MoveState(this);
            DeadState = new DeadState(this);
        }

        public void ChangeState(BaseState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}
