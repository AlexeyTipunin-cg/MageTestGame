using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damage
{
    public class DamageComponent : MonoBehaviour
    {
        private float _damage;
        public void Init(float damage)
        {
            _damage = damage;
        }

        public float GetDamage()
        {
            return _damage;
        }
    }
}