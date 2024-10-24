using Assets.Scripts.ResourceManagement;
using System.Threading.Tasks;

namespace Assets.Scripts.Scene
{
    public class LevelConfigLoader : ILevelConfigLoader
    {
        private readonly IAssetsProvider _assetsProvider;

        public LevelConfigLoader(IAssetsProvider assetsProvider)
        {
            this._assetsProvider = assetsProvider;
        }

        public async Task<LevelConfig> LoadConfig(string configName)
        {
           LevelConfig config = await _assetsProvider.Load<LevelConfig>(configName);
           return config;
        }
    }
}
