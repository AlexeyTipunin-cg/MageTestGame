using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerPosition : MonoBehaviour, IGetPosition
    {
        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}