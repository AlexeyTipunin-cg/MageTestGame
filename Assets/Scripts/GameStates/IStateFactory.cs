
namespace Assets.Scripts.GameStates
{
    public interface IStateFactory
    {
        T CreateState<T>() where T : IGameState;
    }
}