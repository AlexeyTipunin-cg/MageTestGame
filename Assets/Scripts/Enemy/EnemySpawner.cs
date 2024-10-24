using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Assets.Scripts.Enemy;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour, IEnemySpawner
    {
        [SerializeField] private int _enemyMaxCount = 10;

        private PlayerModel _playerModel;
        private ISceneLimits _sceneLimits;
        private int _enemyCounter;
        private ObjectPool<EnemyController> _enemyPool;
        private LevelEnemies _levelEnemies;
        private IEnemyFactory _enemyFactory;

        private int _nextEnemy;


        //private List<Vector3> points = new List<Vector3>();

        [Inject]
        private void Init(IEnemyFactory enemyfactory)
        {
 
            _enemyPool = new ObjectPool<EnemyController>(CreateEnemyToPool, OnGetFromPool, OnRelease, OnDestroyEnemy);
            _enemyFactory = enemyfactory;
        }

        public void SetLevelData(LevelConfig levelConfig, ISceneLimits limits, PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _sceneLimits = limits;
            _levelEnemies = levelConfig.levelEnemies;
        }

        public void StartSpawnerUpdate()
        {
            Observable.EveryUpdate().Where(_ => _enemyCounter < _enemyMaxCount).Subscribe(_ => SpawnEnemy()).AddTo(this);
        }

        private void SpawnEnemy()
        {
            _enemyPool.Get();
        }

        private EnemyController CreateEnemyToPool()
        {
            return CreateEnemy().Result;
        }

        private async Task<EnemyController> CreateEnemy()
        {
            var enemyObject = await _enemyFactory.CreateEnemy(EnemyTypes.Capsule);
            EnemyController enemy = enemyObject.GetComponent<EnemyController>();
            return enemy;
        }

        private void OnGetFromPool(EnemyController enemy)
        {
            enemy.gameObject.SetActive(true);

            EnemyModel model = MakeModel(enemy);
            enemy.Init(model, _playerModel);
            model.isDead.Subscribe(isDead =>
            {
                if (isDead)
                {
                    _enemyPool.Release(enemy);
                }
            }).AddTo(enemy.disposable);

            Vector3 spawnPosition = _sceneLimits.SpawnPosition();
            enemy.transform.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;

            _enemyCounter++;
        }

        private EnemyModel MakeModel(EnemyController enemy)
        {
            EnemyConfig config = _levelEnemies.enemies.First(e => e.enemyType == enemy.EnemyType);
            EnemyModel enemyModel = new EnemyModel(config.health, config);
            return enemyModel;
        }

        private void OnRelease(EnemyController enemy)
        {
            enemy.gameObject.SetActive(false);
            enemy.Dispose();
            _enemyCounter--;
            _enemyCounter = Math.Max(_enemyCounter, 0);
        }

        private void OnDestroyEnemy(EnemyController enemy)
        {
            Destroy(enemy);
        }
    }
}
