using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using Skills;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerInputController _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private LevelConfig _levelConfig;
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        Container.Bind<LevelConfig>().FromScriptableObject(_levelConfig).AsSingle();
        Container.Bind<PlayerModel>().AsSingle();

        Container.Bind<ISceneLimits>().To<SceneController>().FromInstance(_sceneController).AsSingle();

        PlayerInputController player = Container.InstantiatePrefabForComponent<PlayerInputController>(_player,
            _playerSpawn.position, Quaternion.identity,
            null);

        Container.Bind<IPlayerInput>().To<PlayerInputController>().FromInstance(player).AsSingle();
        Container.Bind<IGetPosition>().To<PlayerPosition>().FromComponentOn(player.gameObject).AsSingle();
        Container.Bind<SkillController>().FromComponentOn(player.gameObject).AsSingle();
        // Container.BindInterfacesAndSelfTo<PlayerInputController>().FromInstance(player).AsSingle();

    }
}