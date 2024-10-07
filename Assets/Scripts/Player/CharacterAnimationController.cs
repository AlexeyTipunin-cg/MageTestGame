using UniRx;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _runSpeedAnimation;

        [ContextMenu("Run animation")]

        public void RunAnimation()
        {
            _animator.SetBool("IsRunning", true);
            _animator.speed = _runSpeedAnimation;
        }

        [ContextMenu("Idle animation")]

        public void IdleAnimation()
        {
            _animator.SetBool("IsRunning", false);
            _animator.speed = 1;
        }

        public void DieAnimation()
        {
            _animator.SetBool("IsDie", true);
            _animator.speed = 1;
        }
    }
}