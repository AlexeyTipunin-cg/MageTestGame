using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "GameConfigs/GameCharacters/EnemyCreature", order = 0)]

    public class EnemyConfig : CreatureConfig
    {
        public EnemyTypes enemyType;
        public float damage;
        public float damageInterval = 1;
    }
}