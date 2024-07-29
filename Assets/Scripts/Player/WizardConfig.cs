using Skills;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "WizardConfig", menuName = "GameConfigs/GameCharacters/Wizard", order = 0)]
    public class WizardConfig : ScriptableObject
    {
        public int health = 100;
        public int movementSpeed = 10;
        public int rotationSpeed = 360;
        public int armor = 20;
        public Skill[] Skills;
    }
}