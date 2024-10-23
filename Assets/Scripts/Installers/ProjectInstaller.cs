using Assets.Scripts.ResourceManagement;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AssetsProviderService>().AsSingle();
        }
    }
}
