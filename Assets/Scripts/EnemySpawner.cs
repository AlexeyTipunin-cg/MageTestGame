using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 10;
    [SerializeField] private Enemy _enemy;

    private CharacterController _character;
    private void Awake()
    {
        _character = FindObjectOfType<CharacterController>();
        Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(_ => SpawnEnemy()).AddTo(this);
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(_enemy, Position(), Quaternion.identity);
        enemy.Init(_character);
    }

    private Vector3 Position()
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
