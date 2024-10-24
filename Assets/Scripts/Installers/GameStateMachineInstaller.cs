using Assets.Scripts.GameStates;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BooststrapState>().AsSingle().NonLazy();
            Container.Bind<CoreGameState>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }
    }
}
