using Assets.Scripts.Player;
using Assets.Scripts.ResourceManagement;
using Assets.Scripts.Scene;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawnerFactory : IEnemySpawnerFactory
    {
        private const string PREFAB_NAME = "EnemySpawner";

        private readonly DiContainer _container;
        private readonly IAssetsProvider _assetsProvider;

        public EnemySpawnerFactory(DiContainer container, IAssetsProvider assetsProvider)
        {
            _container = container;
            _assetsProvider = assetsProvider;
        }
        public async Task<IEnemySpawner> CreateEnemySpawner(LevelConfig levelConfig, ISceneLimits limits, PlayerModel playerModel)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(PREFAB_NAME);
            GameObject spawnerGameObject = GameObject.Instantiate(prefab);
            _container.InjectGameObject(spawnerGameObject);
            var spawner = spawnerGameObject.GetComponent<IEnemySpawner>();

            spawner.SetLevelData(levelConfig, limits, playerModel);
            spawner.StartSpawnerUpdate();
            return spawner;
        }
    }
}
