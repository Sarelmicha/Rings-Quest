using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Happyflow.Core.AssetLoader
{
    /// <summary>
    /// An implementation of <see cref="IAssetLoaderService"/>
    /// </summary>
    public class AssetLoaderService : IAssetLoaderService
    {
        private readonly Dictionary<Type, ILoader> m_Loaders;
        private readonly AssetLoaderType m_AssetLoaderType;
        private readonly AssetMapping m_AssetMapping;

        /// <summary>
        /// Constructor for <see cref="AssetLoaderService"/>
        /// </summary>
        /// <param name="assetLoaderType"><see cref="m_AssetLoaderType"/>.</param>
        /// <param name="assetMapping"><see cref="AssetMapping"/>.</param>
        public AssetLoaderService(AssetLoaderType assetLoaderType, AssetMapping assetMapping)
        {
            m_Loaders = new Dictionary<Type, ILoader>();
            m_AssetLoaderType = assetLoaderType;
            m_AssetMapping = assetMapping;
        }

        /// <summary>
        /// Loads the asset with the specified address asynchronously.
        /// </summary>
        /// <typeparam name="TObject">The type of the asset to load.</typeparam>
        /// <param name="name">The name of the asset to load.</param>
        /// <returns>The loaded asset as a UniTask.</returns>
        public async UniTask<TObject> LoadAssetAsync<TObject>(string name)
        {
            Type type = typeof(TObject);
            IAssetLoader<TObject> assetLoader;

            var address = m_AssetMapping.GetAssetAddress(name);
            
            if (string.IsNullOrEmpty(address))
            {
                return default;
            }

            if (!m_Loaders.TryGetValue(type, out var loader))
            {
                assetLoader = AssetLoaderFactory.Create<TObject>(m_AssetLoaderType);
                m_Loaders[type] = assetLoader;
            }
            else
            {
                assetLoader = (IAssetLoader<TObject>)loader;
            }
            
            return await assetLoader.LoadAssetAsync(address);
        }
        
        /// <summary>
        /// Unloads the asset with the specified address.
        /// </summary>
        /// <param name="name">The address of the asset to unload.</param>
        public void UnloadAsset<TObject>(string name)
        {
            Type type = typeof(TObject);
            
            var address = m_AssetMapping.GetAssetAddress(name);
            
            if (string.IsNullOrEmpty(address))
            {
                return;
            }
            
            if (m_Loaders.TryGetValue(type, out var loader))
            {
                var assetLoader = (IAssetLoader<TObject>)loader;
                assetLoader.UnloadAsset(address);

                if (assetLoader.LoadedObjectsCount == 0)
                {
                    m_Loaders.Remove(type);
                }
            }
            else
            {
                Debug.Log($"There is no existing loader with type {nameof(type)} that can be unload.");
            }
        }
    }
}