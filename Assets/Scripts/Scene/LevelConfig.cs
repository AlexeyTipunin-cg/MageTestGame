using Enemy;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "GameConfigs/Levels/LevelConfig", order = 0)]

    public class LevelConfig : ScriptableObject
    {
        public CreatureConfig playerConfig;
        public LevelEnemies levelEnemies;
        [SerializeField][Range(0, 100f)] private float _sceneRadius;

        public float SceneRadius { get => _sceneRadius; }
    }
}