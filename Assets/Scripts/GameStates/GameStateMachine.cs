using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.GameStates
{
    public class GameStateMachine : IInitializable
    {
        private IGameState _currentState;

        private Dictionary<string, IGameState> _cachedStates;
        private readonly IStateFactory _stateFactory;

        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
        void IInitializable.Initialize()
        {
            _cachedStates = new Dictionary<string, IGameState> {
                {nameof(BooststrapState), _stateFactory.CreateState<BooststrapState>() },
                {nameof(CoreGameState), _stateFactory.CreateState<CoreGameState>() }
            };

            ChangeState(nameof(BooststrapState));
        }

        public void ChangeState(string stateName)
        {
            _currentState?.Exit();

            _currentState = _cachedStates[stateName];

            _currentState?.Enter();
        }
    }
}
