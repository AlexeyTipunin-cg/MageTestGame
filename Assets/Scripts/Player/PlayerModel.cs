using Assets.Scripts.Scene;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerModel
    {
        public IReadOnlyReactiveProperty<float> health;
        public IReadOnlyReactiveProperty<bool> isDead;

        private CreatureConfig _creatureConfig;
        private ReactiveProperty<float> _health;
        public int MaxHealth { get; }

        public PlayerModel(LevelConfig config)
        {
            _creatureConfig = config.playerConfig;
            MaxHealth = _creatureConfig.health;
            _health = new ReactiveProperty<float>(_creatureConfig.health);
            health = _health;
            isDead = health.Select(x => x <= 0).ToReactiveProperty();
        }

        public void SetHealth(float addHealth)
        {
            _health.Value = Mathf.Min(health.Value + addHealth, MaxHealth);
        }

        public void AddDamage(float damage)
        {
            float newHealth = health.Value - damage * (1 - _creatureConfig.armor / 100);

            _health.Value = Mathf.Max(health.Value - damage, 0);
        }
    }
}