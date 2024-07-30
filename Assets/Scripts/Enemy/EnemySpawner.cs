using System;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int _enemyMaxCount = 10;
        [SerializeField] private Enemy _enemy;

        private PlayerModel _playerModel;
        private IPlayerInput _playerInput;
        private IGetPosition _getPosition;
        private ISceneLimits _sceneLimits;
        private int _enemyCounter;

        [Inject]
        private void Init(IPlayerInput input, IGetPosition playerPos, ISceneLimits limits, PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _playerInput = input;
            _getPosition= playerPos;
            _sceneLimits = limits;
        
            Observable.Interval(TimeSpan.FromSeconds(2)).Where(_ => _enemyCounter < _enemyMaxCount)
                .Subscribe(_ => SpawnEnemy()).AddTo(this);
        }

        private void SpawnEnemy()
        {
            Enemy enemy = Instantiate(_enemy, _sceneLimits.SpawnPosition(), Quaternion.identity);
            EnemyModel model = new EnemyModel(100);
            enemy.Init(_playerInput, _getPosition, model, _playerModel);
            enemy.OnDestroyAsObservable().Subscribe(_ => OnEnemyDestroy()).AddTo(this);
        
            _enemyCounter++;
        }

        private void OnEnemyDestroy()
        {
            _enemyCounter--;
            _enemyCounter = Math.Max(_enemyCounter, 0);
        }
    }
}
