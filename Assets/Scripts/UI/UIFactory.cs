using Assets.Scripts.Player;
using Assets.Scripts.ResourceManagement;
using DefaultNamespace;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    class UIFactory : IUIFactory
    {
        private const string HUD_NAME = "HUD";
        private readonly DiContainer _container;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISkillsFactory _skillFactory;

        public UIFactory(DiContainer container, IAssetsProvider assetsProvider, ISkillsFactory skillFactory)
        {
            _container = container;
            _assetsProvider = assetsProvider;
            _skillFactory = skillFactory;
        }
        public async Task<HUDController> CreateHUD(PlayerModel playerModel)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(HUD_NAME);

            GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
            _container.InjectGameObject(gameObject);

            HUDController hud = gameObject.GetComponentInChildren<HUDController>();

            hud.SetUpSkillIcons(_skillFactory.SkillController);
            hud.Launch(playerModel);
            return hud;
        }

    }
}
