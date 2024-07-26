using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class AttackController : MonoBehaviour, IAttack
    {
        [SerializeField] private Attack[] _attacks; 
        public void Attack()
        {
            _attacks[0].AttackPlay();
        }
    }
}