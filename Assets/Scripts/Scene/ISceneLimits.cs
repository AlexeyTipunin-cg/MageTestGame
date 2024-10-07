using UnityEngine;

namespace Assets.Scripts.Scene
{
    public interface ISceneLimits
    {
        bool isInsideScene(Vector3 pos);
        Vector3 SpawnPosition();
    }
}