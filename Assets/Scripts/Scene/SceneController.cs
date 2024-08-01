using System;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    public class SceneController : MonoBehaviour, ISceneLimits
    {
        [Range(0f, 100f)]
        [SerializeField] private float _sceneRadius;

        private Vector2 _sceneCenter;

        private void Start()
        {
            _sceneCenter = new Vector2(transform.position.x, transform.position.z);
        }

        public bool isInsideScene(Vector3 pos)
        {
            return Vector2.Distance(_sceneCenter, new Vector2(pos.x, pos.z)) < _sceneRadius;
        }

        public Vector3 SpawnPosition()
        {
            Vector3 randomPoint = RandomPointOnCircleEdge(_sceneRadius);
            return randomPoint;
        }

        private Vector3 RandomPointOnCircleEdge(float radius)
        {
            var vector2 = UnityEngine.Random.insideUnitCircle.normalized * radius;
            return new Vector3(vector2.x, 1.5f, vector2.y);
        }
    }
}
