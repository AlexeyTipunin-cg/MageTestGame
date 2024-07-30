using System;
using System.Collections.Generic;
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
        [SerializeField] private Enemy _enemy;

        private PlayerModel _playerModel;
        private IGetPosition _getPosition;
        private ISceneLimits _sceneLimits;
        private int _enemyCounter;
        private ObjectPool<Enemy> _enemyPool;

        //private List<Vector3> points = new List<Vector3>();

        [Inject]
        private void Init( IGetPosition playerPos, ISceneLimits limits, PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _getPosition = playerPos;
            _sceneLimits = limits;
            _enemyPool = new ObjectPool<Enemy>(CreateEnemy, OnGetFromPool, OnRelease, OnDestroyEnemy);

            Observable.Interval(TimeSpan.FromSeconds(2)).Where(_ => _enemyCounter < _enemyMaxCount)
                .Subscribe(_ => SpawnEnemy()).AddTo(this);
        }

        private void SpawnEnemy()
        {
            _enemyPool.Get();
        }

        private Enemy CreateEnemy()
        {
            Enemy enemy = Instantiate(_enemy);
            return enemy;
        }

        private void OnGetFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);

            EnemyModel model = new EnemyModel(100);
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
