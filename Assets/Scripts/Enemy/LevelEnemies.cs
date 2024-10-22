using System.Collections;
using UnityEngine;

namespace Enemy
{

    [CreateAssetMenu(fileName = "LevelEnemies", menuName = "GameConfigs/LevelCofigs/Enemies", order = 0)]

    public class LevelEnemies : ScriptableObject
    {
        public EnemyConfig[] enemies;
    }
}