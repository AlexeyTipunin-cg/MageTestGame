using System.Collections;
using UnityEngine;
using Cinemachine;
using Zenject;
using Assets.Scripts.Player;
using Assets.Scripts.UI;

namespace Assets.Scripts
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        public void OnHeroCreated(GameObject hero)
        {
            _camera.Follow = _camera.LookAt = hero.transform;
        }

    }
}