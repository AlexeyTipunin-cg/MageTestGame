
using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.ResourceManagement;
using Assets.Scripts.Scene;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.GameStates
{
    public class CoreGameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ILevelConfigLoader _levelConfigLoader;
        private readonly IHeroFactory _hero;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IEnemySpawnerFactory _enemySpawnerFactory;
        private readonly ISkillsFactory _skillsFactory;

        public CoreGameState(GameStateMachine stateMachine, 
            IAssetsProvider assetsProvider, ILevelConfigLoader levelConfigLoader, IHeroFactory hero, IEnemyFactory enemyFactory, IUIFactory uIFactory, IEnemySpawnerFactory enemySpawnerFactory, ISkillsFactory skillsFactory)
        {
            _stateMachine = stateMachine;
            _assetsProvider = assetsProvider;
            _levelConfigLoader = levelConfigLoader;
            _hero = hero;
            _enemyFactory = enemyFactory;
            _uiFactory = uIFactory;
            _enemySpawnerFactory = enemySpawnerFactory;
            _skillsFactory = skillsFactory;
        }
        public async void Enter()
        {
            await _assetsProvider.LoadScene(SceneName.Level_1, OnLoaded);
        }

        private async void OnLoaded(SceneName name)
        {
            LevelConfig config = await _levelConfigLoader.LoadConfig(SceneName.Level_1.ToString());
            ISceneLimits sceneLimits = new SceneUtils(config);
            PlayerModel playerModel = new PlayerModel(config);

            GameObject hero = await _hero.CreateHero(new Vector3(0, 0.519999981f, 0), config, sceneLimits, playerModel);
            await _skillsFactory.CreateSkillController(config, playerModel);
            await _uiFactory.CreateHUD(playerModel);
            await _enemyFactory.LoadEnemies();
            await _enemySpawnerFactory.CreateEnemySpawner(config, sceneLimits, playerModel);

            PlayerCamera playerCamera = Object.FindObjectOfType<PlayerCamera>();
            playerCamera.OnHeroCreated(hero);
            //_camera.OnHeroCreated(hero);
            //_enemySpawner.StartSpawnerUpdate();
        }

        public void Exit()
        {
        }
    }
}
