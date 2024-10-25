using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.ResourceManagement;
using Assets.Scripts.Scene;
using Assets.Scripts.UI;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AssetsProviderService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelConfigLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<HeroFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkillsFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
        }
    }
}
