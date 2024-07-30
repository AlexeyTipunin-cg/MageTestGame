using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int _enemyMaxCount = 10;
        [SerializeField] private Enemy[] _enemies;

        private PlayerModel _playerModel;
        private IGetPosition _getPosition;
        private ISceneLimits _sceneLimits;
        private int _enemyCounter;
        private ObjectPool<Enemy> _enemyPool;
        private LevelEnemies _levelEnemies;

        private int _nextEnemy;


        //private List<Vector3> points = new List<Vector3>();

        [Inject]
        private void Init(LevelEnemies enemies, IGetPosition getPosition, ISceneLimits limits, PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _sceneLimits = limits;
            _levelEnemies = enemies;
            _enemyPool = new ObjectPool<Enemy>(CreateEnemy, OnGetFromPool, OnRelease, OnDestroyEnemy);
            _getPosition = getPosition;
            Observable.EveryUpdate().Where(_ => _enemyCounter < _enemyMaxCount)
                .Subscribe(_ => SpawnEnemy()).AddTo(this);
        }

        private void SpawnEnemy()
        {
            _enemyPool.Get();
        }

        private Enemy CreateEnemy()
        {
            if (_nextEnemy > _enemies.Length - 1)
            {
                _nextEnemy = 0;
            }


            Enemy enemy = Instantiate(_enemies[_nextEnemy]);
            _nextEnemy++;
            return enemy;
        }

        private void OnGetFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);

            EnemyModel model = MakeModel(enemy);
            enemy.Init(_getPosition, model, _playerModel);
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
