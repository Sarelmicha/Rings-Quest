using System;
using Happyflow.Core.AssetLoader;
using Happyflow.Core.Network;
using Happyflow.Core.ServiceLocator;
using UnityEngine;

namespace Happyflow
{
    /// <summary>
    /// Bootstrap the service locator
    /// </summary>
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private AssetMapping m_AssetMapping;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Register all your services next.
            ServiceLocator.Instance.Register<INetworkService>(new HttpClientService("http://localhost:3555"));
            ServiceLocator.Instance.Register<IAssetLoaderService>(new AssetLoaderService(AssetLoaderType.Addressable, m_AssetMapping));
            
            // Application is ready to start, load your main scene.
           // SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
    }
}