using ModestTree;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private CharacterWizardController _player;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerSpawn;
        public override void InstallBindings()
        {
            Debug.Log("Mono Installler");
            // Container.Bind<IAttack>().FromComponentInNewPrefab(_player);
            Container.Bind<Camera>().FromInstance(_camera).AsSingle().NonLazy();
            CharacterWizardController player = Container.InstantiatePrefabForComponent<CharacterWizardController>(_player,
                _playerSpawn.position, Quaternion.identity,
                null);
            Container.Bind<CharacterWizardController>().FromInstance(player).AsSingle().NonLazy();
        }
    }
}