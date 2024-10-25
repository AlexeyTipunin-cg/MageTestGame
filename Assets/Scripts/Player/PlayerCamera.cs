using UnityEngine;
using Cinemachine;

namespace Assets.Scripts
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        public void OnHeroCreated(Transform hero)
        {
            _camera.Follow = _camera.LookAt = hero;
        }

    }
}