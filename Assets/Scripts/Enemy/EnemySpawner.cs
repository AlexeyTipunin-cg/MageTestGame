using System;
using DefaultNamespace;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRadius = 10;
        [SerializeField] private int _enemyMaxCount = 10;
        [SerializeField] private Enemy _enemy;

        private PlayerModel _playerModel;
        private IPlayerInput _playerInput;
        private int _enemyCounter;

        [Inject]
        private void Init(IPlayerInput input, PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _playerInput = input;
        
            Observable.Interval(TimeSpan.FromSeconds(2)).Where(_ => _enemyCounter < _enemyMaxCount)
                .Subscribe(_ => SpawnEnemy()).AddTo(this);
        }

        private void SpawnEnemy()
        {
            Enemy enemy = Instantiate(_enemy, SpawnPosition(), Quaternion.identity);
            EnemyModel model = new EnemyModel(100);
            enemy.Init(_playerInput, model, _playerModel);
            enemy.OnDestroyAsObservable().Subscribe(_ => OnEnemyDestroy()).AddTo(this);
        
            _enemyCounter++;
        }

        private void OnEnemyDestroy()
        {
            _enemyCounter--;
            _enemyCounter = Math.Max(_enemyCounter, 0);
        }
    
    

        private Vector3 SpawnPosition()
        {
            Vector3 randomPoint = RandomPointOnCircleEdge(_spawnRadius);
            return randomPoint;
        }
    
        private Vector3 RandomPointOnCircleEdge(float radius)
        {
            var vector2 = Random.insideUnitCircle.normalized * radius;
            return new Vector3(vector2.x, 1.5f, vector2.y);
        }
    
    
    }
}
