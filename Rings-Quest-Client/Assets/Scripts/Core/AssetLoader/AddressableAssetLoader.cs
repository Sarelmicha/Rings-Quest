using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Happyflow.Core.AssetLoader
{
    /// <summary>
    /// Am implementation of <see cref="IAssetLoader{TObject}"/> using Addressables.
    /// </summary>
    public class AddressableAssetLoader<TObject> : IAssetLoader<TObject>, IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle<TObject>> m_LoadedAssetsOperations;
        
        /// <summary>
        /// Gets the count of loaded objects.
        /// </summary>
        public int LoadedObjectsCount => m_LoadedAssetsOperations.Count;

        public AddressableAssetLoader()
        {
            m_LoadedAssetsOperations = new Dictionary<string, AsyncOperationHandle<TObject>>();
        }

        /// <summary>
        /// Loads the asset with the specified address asynchronously.
        /// </summary>
        /// <typeparam name="TObject">The type of the asset to load.</typeparam>
        /// <param name="address">The address of the asset to load.</param>
        /// <returns>The loaded asset as a UniTask.</returns>
        public async UniTask<TObject> LoadAssetAsync(string address)
        {
            if (!m_LoadedAssetsOperations.TryGetValue(address, out var asyncOperationHandle))
            {
                asyncOperationHandle = Addressables.LoadAssetAsync<TObject>(address);
                m_LoadedAssetsOperations[address] = asyncOperationHandle;
            }
            
            if (!asyncOperationHandle.IsDone)
            {
                await asyncOperationHandle.Task;
            }
                
            return asyncOperationHandle.Status == AsyncOperationStatus.Succeeded ? asyncOperationHandle.Result : default;
        }

        /// <summary>
        /// Unloads the asset with the specified address.
        /// </summary>
        /// <param name="address">The address of the asset to unload.</param>
        public void UnloadAsset(string address)
        {
            if (m_LoadedAssetsOperations.TryGetValue(address, out var handle))
            {
                Addressables.Release(handle);
                m_LoadedAssetsOperations.Remove(address);
            }
            else
            {
                Debug.Log($"There is no existing loader with the address {address} that can be unload.");
            }
        }
        
        /// <summary>
        /// Dispose all <see cref="AddressableAssetLoader{TObject}"/> components.
        /// </summary>
        public void Dispose()
        {
            ClearLoadedAssets();
        }

        private void ClearLoadedAssets()
        {
            foreach (var handle in m_LoadedAssetsOperations.Values)
            {
                Addressables.Release(handle);
            }

            m_LoadedAssetsOperations.Clear();
        }
    }
}