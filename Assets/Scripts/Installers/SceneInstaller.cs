using Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using Skills;
using UnityEngine;
using Zenject;
using Cinemachine;
using Assets.Scripts.UI;
using Assets.Scripts;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerInputController _playerInput;
    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private PlayerCamera _playerCamera;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerInputController>().FromComponentInNewPrefab(_playerInput).AsSingle();

        Container.Bind<LevelConfig>().FromScriptableObject(_levelConfig).AsSingle();
        Container.Bind<PlayerModel>().AsSingle();

        Container.Bind<ISceneLimits>().To<SceneController>().FromInstance(_sceneController).AsSingle();
        Container.BindInterfacesAndSelfTo<HeroFactory>().AsSingle().WithArguments(_playerSpawn.position);
        Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemySpawner>().FromComponentOn(_enemySpawner.gameObject).AsSingle();
        Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerCamera>().FromComponentOn(_playerCamera.gameObject).AsSingle();

        Container.BindInterfacesAndSelfTo<LoadLevel>().AsSingle().NonLazy();

        //GameObject player = Container.InstantiatePrefab(_player,
        //    _playerSpawn.position, Quaternion.identity,
        //    null);

        //Container.Bind<IPlayerInput>().To<PlayerInputController>().FromComponentOn(player).AsSingle();
        //Container.BindInterfacesAndSelfTo<PlayerPosition>().FromComponentOn(player).AsSingle();
        //Container.Bind<ISkillController>().To<SkillController>().FromComponentOn(player).AsSingle();
    }
}