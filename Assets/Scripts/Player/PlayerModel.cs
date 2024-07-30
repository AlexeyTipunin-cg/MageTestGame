using UniRx;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerModel
    {
        public ReactiveProperty<float> health;
        public IReadOnlyReactiveProperty<bool> isDead;
        public int MaxHealth { get; }

        public PlayerModel(WizardConfig config)
        {
            MaxHealth = config.health;
            health = new ReactiveProperty<float>(config.health);
            isDead = health.Select(x => x <= 0).ToReactiveProperty();
        }

        public void SetHealth(float addHealth)
        {
            health.Value = Mathf.Min(health.Value + addHealth, MaxHealth);
        }
    }
}