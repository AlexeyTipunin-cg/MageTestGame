namespace Assets.Scripts.Player
{
    public abstract class BaseState
    {
        public PlayerStateMachine _stateMachine;
        public virtual void Enter()
        {
            AddInputCallbacks();
        }
        public virtual void Exit()
        {
            RemoveInputCallbacks();
        }

        public abstract void Update();
        public abstract void FixedUpdate();

        public abstract void AddInputCallbacks();
        public abstract void RemoveInputCallbacks();

        protected BaseState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}
