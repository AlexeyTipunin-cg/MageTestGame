using Assets.Scripts.ResourceManagement;
using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public UIFactory(DiContainer container, IAssetsProvider assetsProvider)
        {
            _container = container;
            _assetsProvider = assetsProvider;
        }
        public async Task<HUDController> CreateHUD()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(HUD_NAME);
            HUDController hud = _container.InstantiatePrefab(prefab).GetComponentInChildren<HUDController>();
            return hud;
        }

    }
}
