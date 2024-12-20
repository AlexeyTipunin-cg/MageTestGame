using Assets.Scripts.Damage;
using Assets.Scripts.Enemy;
using UnityEngine;

namespace Enemy
{
    public class Damageable : MonoBehaviour
    {
        private EnemyModel _model;

        public void Init(EnemyModel model)
        {
            _model = model;
        }
        public void AddDamage(float damage)
        {
            _model.AddDamage(damage);
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out DamageComponent damage))
            {
                _model.AddDamage(damage.GetDamage());
            }
        }
    }
}