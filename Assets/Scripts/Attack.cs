using UnityEngine;

namespace DefaultNamespace
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void AttackPlay()
        {
            _particleSystem.Play();
        }
    }
}