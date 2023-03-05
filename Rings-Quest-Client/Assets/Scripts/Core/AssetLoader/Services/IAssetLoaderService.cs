using Cysharp.Threading.Tasks;
using Happyflow.Core.ServiceLocator;

namespace Happyflow.Core.AssetLoader
{
    /// <summary>
    /// An interface for a service that provides methods for loading and unloading assets.
    /// </summary>
    public interface IAssetLoaderService : IGameService
    {
        /// <summary>
        /// Loads the asset with the specified address asynchronously.
        /// </summary>
        /// <typeparam name="TObject">The type of the asset to load.</typeparam>
        /// <param name="name">The name of the asset to load.</param>
        /// <returns>The loaded asset as a UniTask.</returns>
        UniTask<TObject> LoadAssetAsync<TObject>(string name);

        /// <summary>
        /// Unloads the asset with the specified address.
        /// </summary>
        /// <param name="name">The name of the asset to unload.</param>
        void UnloadAsset<TObject>(string name);
    }
}