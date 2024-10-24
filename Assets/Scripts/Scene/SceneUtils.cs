using UnityEngine;

namespace Assets.Scripts.Scene
{
    public class SceneUtils : ISceneLimits
    {
        private readonly LevelConfig _levelConfig;

        private Vector2 _sceneCenter;

        public SceneUtils(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public bool isInsideScene(Vector3 pos)
        {
            return Vector2.Distance(Vector2.zero, new Vector2(pos.x, pos.z)) < _levelConfig.SceneRadius;
        }

        public Vector3 SpawnPosition()
        {
            Vector3 randomPoint = RandomPointOnCircleEdge(_levelConfig.SceneRadius);
            return randomPoint;
        }

        private Vector3 RandomPointOnCircleEdge(float radius)
        {
            var vector2 = UnityEngine.Random.insideUnitCircle.normalized * radius;
            return new Vector3(vector2.x, 1.5f, vector2.y);
        }
    }
}
