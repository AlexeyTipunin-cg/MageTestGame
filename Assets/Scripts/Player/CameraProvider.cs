using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class CameraProvider : ICameraProvider
    {
        public PlayerCamera GetCamera()
        {
            return Object.FindObjectOfType<PlayerCamera>();
        }
    }
}
