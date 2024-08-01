using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "GameConfigs/Levels/LevelConfig", order = 0)]

    public class LevelConfig : ScriptableObject
    {
        public CreatureConfig playerConfig;
        public LevelEnemies levelEnemies;
    }
}