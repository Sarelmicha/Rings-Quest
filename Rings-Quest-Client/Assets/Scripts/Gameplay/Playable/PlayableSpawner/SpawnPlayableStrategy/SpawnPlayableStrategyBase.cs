using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Happyflow.Core.AssetLoader;
using Happyflow.Core.EventAggregator;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable
{
    /// <summary>
    /// Base class for all the playable spawner strategies.
    /// </summary>
    public abstract class SpawnPlayableStrategyBase : MonoBehaviour, ISpawnPlayableStrategy 
    {
        protected IVerticesMapService m_VerticesMapService;
        protected IAssetLoaderService m_AssetLoaderService;
        protected Dictionary<string, int> m_PlayablesCountMap;
        protected IEventAggregator m_EventAggregator;

        /// <summary>
        /// /// Initialize the spawn playable strategy
        /// </summary>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        /// <param name="assetLoaderService"><see cref="AssetLoaderService"/> instance.</param>
        /// <param name="playablesCountMap">Map the playables by name and count for a specific gameplay.</param>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/></param>
        /// <returns>True if Initialized properly, otherwise returns False.</returns>
        public virtual async UniTask<bool> Initialize(IVerticesMapService verticesMapService, IAssetLoaderService assetLoaderService, 
            Dictionary<string, int> playablesCountMap, IEventAggregator eventAggregator)
        {
            if (verticesMapService == null || assetLoaderService == null || playablesCountMap == null || eventAggregator == null)
            {
                Debug.Log($"Cannot Initialize {nameof(ThresholdPoolingStrategy)} with an invalid data.");
                return false;
            }

            m_VerticesMapService = verticesMapService;
            m_AssetLoaderService = assetLoaderService;
            m_PlayablesCountMap = playablesCountMap;
            m_EventAggregator = eventAggregator;

            return true;
        }
        
        /// <summary>
        /// Spawn a single <see cref="Playable"/>
        /// </summary>
        /// <param name="playableDTO">The playableDTO of the playable to spawn</param>
        public abstract UniTask Spawn(PlayableDTO playableDTO);

        /// <summary>
        /// Dispose all of the components of the <see cref="SpawnPlayableStrategyBase"/>
        /// </summary>
        public abstract void Dispose();
    }
}