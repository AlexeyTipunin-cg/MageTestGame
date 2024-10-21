using System.Collections;
using UnityEngine;
using Cinemachine;
using Zenject;
using Assets.Scripts.Player;

namespace Assets.Scripts
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        [Inject]
        public void Init(IPlayerTransform playerTransform)
        {
            _camera.Follow = _camera.LookAt = playerTransform.GetTransform();
        }

    }
}