using DefaultNamespace;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class WallDetection : MonoBehaviour
    {
        private bool _bumpInWall;

        public bool BumpInWall => _bumpInWall;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == (int)GameLayers.Walls)
            {
                _bumpInWall = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == (int)GameLayers.Walls)
            {
                _bumpInWall = false;
            }
        }
    }
}