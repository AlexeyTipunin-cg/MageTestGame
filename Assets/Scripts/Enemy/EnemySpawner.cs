using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int _enemyMaxCount = 10;

        private PlayerModel _playerModel;
        private ISceneLimits _sceneLimits;
        private int _enemyCounter;
        private ObjectPool<Enemy> _enemyPool;
        private LevelEnemies _levelEnemies;
        private IEnemyFactory _enemyFactory;

        private int _nextEnemy;


        //private List<Vector3> points = new List<Vector3>();

        [Inject]
        private void Init(LevelConfig levelConfig, ISceneLimits limits, PlayerModel playerModel, IEnemyFactory enemyfactory)
        {
            _playerModel = playerModel;
            _sceneLimits = limits;
            _levelEnemies = levelConfig.levelEnemies;
            _enemyPool = new ObjectPool<Enemy>(CreateEnemyToPool, OnGetFromPool, OnRelease, OnDestroyEnemy);
            _enemyFactory = enemyfactory;
        }

        public void StartSpawnerUpdate()
        {
            Observable.EveryUpdate().Where(_ => _enemyCounter < _enemyMaxCount).Subscribe(_ => SpawnEnemy()).AddTo(this);
        }

        private void SpawnEnemy()
        {
            _enemyPool.Get();
        }

        private Enemy CreateEnemyToPool()
        {
            return CreateEnemy().Result;
        }

        private async Task<Enemy> CreateEnemy()
        {
            var enemyObject = await _enemyFactory.CreateEnemy(EnemyTypes.Capsule);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            return enemy;
        }

        private void OnGetFromPool(Enemy enemy)
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

        private EnemyModel MakeModel(Enemy enemy)
        {
            EnemyConfig config = _levelEnemies.enemies.First(e => e.enemyType == enemy.EnemyType);
            EnemyModel enemyModel = new EnemyModel(config.health, config);
            return enemyModel;
        }

        private void OnRelease(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            enemy.Dispose();
            _enemyCounter--;
            _enemyCounter = Math.Max(_enemyCounter, 0);
        }

        private void OnDestroyEnemy(Enemy enemy)
        {
            Destroy(enemy);
        }
    }
}
