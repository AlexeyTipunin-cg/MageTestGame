using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerPosition : MonoBehaviour, IGetPosition, IPlayerTransform
    {
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}