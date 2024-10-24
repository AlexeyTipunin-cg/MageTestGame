
using Assets.Scripts.ResourceManagement;
using Unity.VisualScripting;

namespace Assets.Scripts.GameStates
{
    public class BooststrapState : IGameState
    {
        private readonly GameStateMachine _stateMachine;

        public BooststrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            _stateMachine.ChangeState(nameof(CoreGameState));
        }

        public void Exit()
        {
        }
    }
}
