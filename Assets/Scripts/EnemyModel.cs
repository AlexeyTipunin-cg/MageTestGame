using UniRx;

namespace DefaultNamespace
{
    public class EnemyModel
    {
        public ReactiveProperty<float> health;
        public int MaxHealth { get; }

        public EnemyModel(int startHealth)
        {
            MaxHealth = startHealth;
            health = new ReactiveProperty<float>(startHealth);
        }

        public bool IsDead()
        {
            return health.Value <= 0;
        }
    }
}