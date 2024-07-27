using UnityEngine;

namespace DefaultNamespace
{
    public class DamageComponent : MonoBehaviour, IDamage
    {
        public float GetDamage()
        {
            return 50;
        }
    }
}