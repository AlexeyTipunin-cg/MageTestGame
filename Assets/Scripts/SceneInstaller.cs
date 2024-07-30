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
    [SerializeField] private WizardConfig _wizardConfig;
    [SerializeField] private SceneController _sceneController;
    public override void InstallBindings()
    {
        Debug.Log("Mono Installler");
        // Container.Bind<IAttack>().FromComponentInNewPrefab(_player);
        Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        Container.Bind<WizardConfig>().FromInstance(_wizardConfig).AsSingle();
        Container.Bind<PlayerModel>().AsSingle();

        Container.Bind<ISceneLimits>().To<SceneController>().FromInstance(_sceneController).AsSingle();

        PlayerInputController player = Container.InstantiatePrefabForComponent<PlayerInputController>(_player,
            _playerSpawn.position, Quaternion.identity,
            null);
        Container.Bind<IPlayerInput>().To<PlayerInputController>().FromInstance(player).AsSingle();
        Container.Bind<IGetPosition>().To<PlayerPosition>().FromInstance(player.GetComponent<PlayerPosition>()).AsSingle();
        Container.Bind<SkillController>().FromComponentOn(player.gameObject).AsSingle();
        // Container.BindInterfacesAndSelfTo<PlayerInputController>().FromInstance(player).AsSingle();

    }
}