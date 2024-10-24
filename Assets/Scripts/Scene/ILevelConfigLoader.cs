using System.Threading.Tasks;

namespace Assets.Scripts.Scene
{
    public interface ILevelConfigLoader
    {
        Task<LevelConfig> LoadConfig(string configName);
    }
}