using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;
        public override void InstallBindings()
        {
            // Container.Bind<IAttack>().FromComponentInNewPrefab(_player);
        }
    }
}