
using Unity.VisualScripting;
using Zenject;

namespace Assets.Scripts.GameStates
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public T CreateState<T>() where T : IGameState
        {
            return _container.Resolve<T>();
        }
    }
}
