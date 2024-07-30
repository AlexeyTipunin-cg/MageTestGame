using Skills;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class HealSkill : SkillView
    {
        protected override void ApplyInfluenceImmediattly()
        {
            _playerModel.SetHealth(_config.damage);
        }
    }
}