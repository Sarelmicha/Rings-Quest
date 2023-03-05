using Happyflow.Core.AssetLoader;
using UnityEngine;

namespace Happyflow.Core.AssetLoader
{
    /// <summary>
    /// A factory for creating instances of asset loaders.
    /// </summary>
    public static class AssetLoaderFactory
    {
        /// <summary>
        /// Creates an instance of an asset loader of the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type of the asset to be loaded by the created asset loader.</typeparam>
        /// <param name="assetLoaderType">The type of the asset loader to create.</param>
        /// <returns>An instance of the specified asset loader, or `null` if no matching implementation was found.</returns>
        public static IAssetLoader<TObject> Create<TObject>(AssetLoaderType assetLoaderType)
        {
            switch (assetLoaderType)
            {
                case AssetLoaderType.Addressable:
                    return new AddressableAssetLoader<TObject>();
                default:
                    Debug.Log($"There is no implementation matched for this type {assetLoaderType}");
                    break;
            }

            return null;
        }
    }
}