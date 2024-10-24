using Assets.Scripts.Scene;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Assets.Scripts.ResourceManagement
{
    public interface IAssetsProvider
    {
        public Task<T> Load<T>(string key, CancellationToken cancellationToken = default) where T : class;
        public Task<T> Load<T>(AssetReference assetReference, CancellationToken cancellationToken = default) where T : class;

        public Task<SceneInstance> LoadScene(SceneName sceneName, CancellationToken cancellationToken = default);

        public void Release(string key);

        public void Cleanup();
        Task<SceneInstance> LoadScene(SceneName sceneName, Action<SceneName> onLoaded = null, CancellationToken cancellationToken = default);
    }
}
