using UnityEngine;

namespace Assets.Scripts.Scene
{
    internal interface ISceneLimits
    {
        bool isInsideScene(Vector3 pos);
        Vector3 SpawnPosition();
    }
}