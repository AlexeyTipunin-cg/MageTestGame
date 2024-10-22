using Assets.Scripts.Player;
using Assets.Scripts.UI;
using Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    public class LoadLevel
    {
        private readonly IHeroFactory _hero;
        private readonly IEnemyFactory _enemyFactory;
        private readonly EnemySpawner _enemySpawner;
        private readonly PlayerCamera _camera;

        private IUIFactory _uiFactory { get; }

        public LoadLevel(IHeroFactory hero, IEnemyFactory enemyFactory, IUIFactory uIFactory, EnemySpawner enemySpawner, PlayerCamera camera)
        {
            _hero = hero;
            this._enemyFactory = enemyFactory;
            _uiFactory = uIFactory;
            _enemySpawner = enemySpawner;
            _camera = camera;
            StartLevel();
        }

        private async void StartLevel()
        {
            GameObject hero = await _hero.CreateHero();
            await _uiFactory.CreateHUD();
            await _enemyFactory.LoadEnemies();
            _camera.OnHeroCreated(hero);
            _enemySpawner.StartSpawnerUpdate();
        }
    }
}
