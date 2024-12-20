﻿using Assets.Scripts.Player;
using Assets.Scripts.ResourceManagement;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
using Enemy;

namespace Assets.Scripts.Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IHeroFactory _heroPosition;

        public EnemyFactory(DiContainer container, IAssetsProvider assetsProvider, IHeroFactory hero)
        {
            _container = container;
            _assetsProvider = assetsProvider;
            _heroPosition = hero;
        }

        public async Task<GameObject> CreateEnemy(EnemyTypes type)
        {
            GameObject enemyPrefab = await _assetsProvider.Load<GameObject>(type.ToString());
            var enemy = UnityEngine.Object.Instantiate(enemyPrefab);
            _container.InjectGameObject(enemy);

            enemy.GetComponent<EnemyController>().SetupPlayerPosition(_heroPosition.HeroPosition);
            return enemy;
        }

        public async Task LoadEnemies()
        {
            List<Task<GameObject>> tasks = new List<Task<GameObject>>();
            foreach (EnemyTypes item in Enum.GetValues(typeof(EnemyTypes)))
            {
                tasks.Add(_assetsProvider.Load<GameObject>(item.ToString()));
            }

            var task = await Task.WhenAll(tasks);
        }
    }
}
