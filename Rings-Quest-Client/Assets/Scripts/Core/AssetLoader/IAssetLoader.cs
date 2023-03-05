using Cysharp.Threading.Tasks;

namespace Happyflow.Core.AssetLoader
{
    /// <summary>
    /// An interface for loading and unloading Unity objects through Addressables.
    /// </summary>
    public interface IAssetLoader<TObject> : ILoader
    {
        /// <summary>
        /// Loads the asset with the specified address asynchronously.
        /// </summary>
        /// <typeparam name="TObject">The type of the asset to load.</typeparam>
        /// <param name="address">The address of the asset to load.</param>
        /// <returns>The loaded asset as a UniTask.</returns>
        UniTask<TObject> LoadAssetAsync(string address);

        /// <summary>
        /// Unloads the asset with the specified address.
        /// </summary>
        /// <param name="address">The address of the asset to unload.</param>
        void UnloadAsset(string address);
    }
}

