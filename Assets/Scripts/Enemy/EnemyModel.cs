using Enemy;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyModel
    {
        public IReadOnlyReactiveProperty<float> health;
        public IReadOnlyReactiveProperty<bool> isDead;
        private ReactiveProperty<float> _health;

        public int MaxHealth { get; }
        public EnemyConfig Config { get; }

        public EnemyModel(int startHealth, EnemyConfig config)
        {
            Config = config;
            MaxHealth = startHealth;
            _health = new ReactiveProperty<float>(startHealth);
            health = _health;
            isDead = health.Select(h => IsDead()).ToReactiveProperty();
        }

        public bool IsDead()
        {
            return health.Value <= 0;
        }

        public void AddDamage(float damage)
        {
            float newHealth = health.Value - damage * (1 - Config.armor / 100);
            _health.Value = Mathf.Max(newHealth, 0);
        }


    }
}