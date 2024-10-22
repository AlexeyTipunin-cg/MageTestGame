using Assets.Scripts.ResourceManagement;
using Skills;
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

        public HeroFactory(DiContainer container, IAssetsProvider assetsProvider, Vector3 postion)
        {
            _container = container;
            _assetsProvider = assetsProvider;
            _position = postion;
        }

        public IGetHeroPosition HeroPosition => _heroPosition;

        public async Task<GameObject> CreateHero()
        {
            GameObject playerPrefab = await _assetsProvider.Load<GameObject>(PREFAB_NAME);
            var player = _container.InstantiatePrefab(playerPrefab, _position, Quaternion.identity, null);
            _container.Bind<ISkillController>().To<SkillController>().FromComponentOn(player).AsSingle();
            _heroPosition = player.GetComponent<IGetHeroPosition>();
            onPlayerCreated?.Invoke(player);
            return player;
        }
    }
}
