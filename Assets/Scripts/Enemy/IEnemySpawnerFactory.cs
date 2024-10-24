using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy
{
    public interface IEnemySpawnerFactory
    {
        Task<IEnemySpawner> CreateEnemySpawner(LevelConfig levelConfig, ISceneLimits limits, PlayerModel playerModel);
    }
}