using UnityEngine;

namespace Skills
{
    [CreateAssetMenu(fileName = "Skill", menuName = "GameConfigs/Skills/Skill", order = 0)]
    public class Skill : ScriptableObject
    {
        public float damage;
        public float cooldownTimeSec;
        public float radius;
        public SkillType skillType;
        public Color skillIconColor;
    }
}