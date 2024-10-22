using Assets.Scripts.ResourceManagement;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public interface IEnemyFactory
    {
        public Task LoadEnemies();
        public Task<GameObject> CreateEnemy(EnemyTypes type);
    }
}