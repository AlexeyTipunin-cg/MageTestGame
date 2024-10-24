using Assets.Scripts.ResourceManagement;
using Assets.Scripts.Scene;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class HeroFactory : IHeroFactory
    {
        private const string PREFAB_NAME = "Wizard";

        public event Action<GameObject> onPlayerCreated;

        private readonly DiContainer _container;
        private readonly IAssetsProvider _assetsProvider;
        private readonly Vector3 _position;
        private IGetHeroPosition _heroPosition;
        private GameObject _hero;

        public HeroFactory(DiContainer container, IAssetsProvider assetsProvider)
        {
            _container = container;
            _assetsProvider = assetsProvider;
        }

        public IGetHeroPosition HeroPosition => _heroPosition;

        public async Task<GameObject> CreateHero(Vector3 postion, LevelConfig levelConfig, ISceneLimits limits, PlayerModel playerModel)
        {
            GameObject playerPrefab = await _assetsProvider.Load<GameObject>(PREFAB_NAME);
            var player = UnityEngine.Object.Instantiate(playerPrefab, postion, Quaternion.identity, null);
            _container.InjectGameObject(player);

            player.GetComponent<PlayerController>().Launch(playerModel, levelConfig, limits);

            _heroPosition = player.GetComponent<IGetHeroPosition>();
            _hero = player;
            onPlayerCreated?.Invoke(player);
            return player;
        }

        public GameObject GetHero()
        {
            return _hero;
        }
    }
}
