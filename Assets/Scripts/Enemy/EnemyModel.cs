using UniRx;

namespace Enemy
{
    public class EnemyModel
    {
        public ReactiveProperty<float> health;
        public IReadOnlyReactiveProperty<bool> isDead;
        public int MaxHealth { get; }

        public EnemyModel(int startHealth)
        {
            MaxHealth = startHealth;
            health = new ReactiveProperty<float>(startHealth);
            isDead = health.Select(h => IsDead()).ToReactiveProperty();
        }

        public bool IsDead()
        {
            return health.Value <= 0;
        }


    }
}