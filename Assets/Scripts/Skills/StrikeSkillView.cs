using System;
using Enemy;
using UnityEngine;

namespace Skills
{
    public class StrikeSkillView : SkillView
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Damageable damageable))
            {
                damageable.AddDamage(_config.damage);
            }
        }
    }
}