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
            _model.health.Value -= damage;
            if (_model.IsDead())
            {
                Destroy(gameObject);
            }
        }
    }
}