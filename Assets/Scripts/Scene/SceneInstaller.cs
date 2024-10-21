using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using Skills;
using UnityEngine;
using Zenject;
using Cinemachine;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerInputController _player;
    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private LevelConfig _levelConfig;
    public override void InstallBindings()
    {
        Container.Bind<LevelConfig>().FromScriptableObject(_levelConfig).AsSingle();
        Container.Bind<PlayerModel>().AsSingle();

        Container.Bind<ISceneLimits>().To<SceneController>().FromInstance(_sceneController).AsSingle();

        GameObject player = Container.InstantiatePrefab(_player,
            _playerSpawn.position, Quaternion.identity,
            null);

        Container.Bind<IPlayerInput>().To<PlayerInputController>().FromComponentOn(player).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerPosition>().FromComponentOn(player).AsSingle();
        Container.Bind<ISkillController>().To<SkillController>().FromComponentOn(player).AsSingle();
    }
}