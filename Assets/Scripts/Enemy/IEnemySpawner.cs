using Assets.Scripts.Player;
using Assets.Scripts.Scene;

namespace Assets.Scripts.Enemy
{
    public interface IEnemySpawner
    {
        void SetLevelData(LevelConfig levelConfig, ISceneLimits limits, PlayerModel playerModel);
        void StartSpawnerUpdate();
    }
}