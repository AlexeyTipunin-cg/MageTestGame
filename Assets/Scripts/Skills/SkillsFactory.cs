using Assets.Scripts.Player;
using Assets.Scripts.ResourceManagement;
using Assets.Scripts.Scene;
using Skills;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    class SkillsFactory : ISkillsFactory
    {
        private const string SKILL_CONTROLLER_PREFAB = "SkillController";
        private readonly DiContainer _container;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IHeroFactory _heroFactory;

        private ISkillController _skillController;

        public SkillsFactory(DiContainer container, IAssetsProvider assetsProvider, IHeroFactory heroFactory)
        {
            _container = container;
            _assetsProvider = assetsProvider;
            _heroFactory = heroFactory;
        }

        public ISkillController SkillController { get => _skillController; }

        public async Task<ISkillController> CreateSkillController(LevelConfig levelConfig, PlayerModel playerModel)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(SKILL_CONTROLLER_PREFAB);

            GameObject skillGameObject = Object.Instantiate(prefab);
            _container.InjectGameObject(skillGameObject);
            ISkillController skillController = skillGameObject.GetComponent<ISkillController>();
            skillController.Lanch(levelConfig, playerModel);
            _skillController = skillController;
            return skillController;
        }

    }
}
