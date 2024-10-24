using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public interface IEnemyFactory
    {
        public Task LoadEnemies();
        public Task<GameObject> CreateEnemy(EnemyTypes type);
    }
}