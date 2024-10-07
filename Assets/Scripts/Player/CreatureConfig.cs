using Skills;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "CreatureConfig", menuName = "GameConfigs/GameCharacters/SomeCreature", order = 0)]
    public class CreatureConfig : ScriptableObject
    {
        public int health = 100;
        public float movementSpeed = 10;
        public int rotationSpeed = 360;
        [Range(0f, 100f)]
        public int armor = 20;
        public Skill[] Skills;
    }
}