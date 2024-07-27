using System.Collections.Generic;
using UniRx;

namespace DefaultNamespace
{
    public class PlayerModel
    {
        public ReactiveProperty<float> health;
        public IReadOnlyReactiveProperty<bool> isDead;
        public int MaxHealth { get; }

        public PlayerModel(int startHealth)
        {
            MaxHealth = startHealth;
            health = new ReactiveProperty<float>(startHealth);
            isDead = health.Select(x => x <= 0).ToReactiveProperty();
        }
    }
}