using Assets.Scripts.Scene;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Assets.Scripts.ResourceManagement
{
    public class AssetsProviderService : IAssetsProvider
    {

        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public async Task<T> Load<T>(AssetReference assetReference, CancellationToken cancellationToken = default) where T : class
        {
            return await Load<T>(key: assetReference.AssetGUID, cancellationToken: cancellationToken);
        }


        public async Task<T> Load<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            if (_completedCache.TryGetValue(key, out var completeHandle))
            {
                return completeHandle.Result as T;
            }

            cancellationToken.ThrowIfCancellationRequested();
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            cancellationToken.ThrowIfCancellationRequested();

            return await LoadAsset(key, handle, cancellationToken);
        }
        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> handles in _handles.Values)
            {
                foreach (AsyncOperationHandle resHandle in handles)
                {
                    Addressables.Release(resHandle);
                }
            }

            _completedCache.Clear();
            _handles.Clear();
        }

        private async Task<T> LoadAsset<T>(string key, AsyncOperationHandle<T> handle, CancellationToken cancellationToken)
        {
            handle.Completed += completeHandle => _completedCache[key] = completeHandle;

            cancellationToken.ThrowIfCancellationRequested();

            if (!_handles.TryGetValue(key, out var resourseHandles))
            {
                resourseHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourseHandles;
            }
            resourseHandles.Add(handle);

            var result = await handle.Task;
            cancellationToken.ThrowIfCancellationRequested();

            return result;
        }

        public async Task<SceneInstance> LoadScene(SceneName sceneName, CancellationToken cancellationToken = default)
        {
            var operationHandle = Addressables.LoadSceneAsync(sceneName.ToString());
            return await operationHandle.Task;
        }

        public void Release(string key)
        {
            if (!_handles.TryGetValue(key, out var handlesForKey))
            {
                return;
            }

            foreach (var handle in handlesForKey)
            {
                Addressables.Release(handle);
            }

            _completedCache.Remove(key);
            _handles.Remove(key);
        }
    }
}
