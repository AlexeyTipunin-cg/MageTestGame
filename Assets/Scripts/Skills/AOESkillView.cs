using DefaultNamespace;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Skills
{
    public class AOESkillView : SkillView
    {
        protected override void ApplyInfluenceImmediattly()
        {
            Collider[] colliders = new Collider[10];
            int count =
                Physics.OverlapSphereNonAlloc(transform.position, _config.radius, colliders, 1 << (int)GameLayers.Enemy);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Collider c = colliders[i];
                    if (c.TryGetComponent(out Damageable damageable))
                    {
                        damageable.AddDamage(_config.damage);
                    }
                }
            }
        }
    }
}
