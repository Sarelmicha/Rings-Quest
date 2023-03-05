using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Happyflow.Core.AssetLoader;
using Happyflow.Core.EventAggregator;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable
{
    /// <summary>
    /// Handle the logic of spawning the playables in a specific strategy. 
    /// </summary>
    public class PlayableSpawner : MonoBehaviour
    {
        /// <summary>
        /// The strategy for spawning the playables.
        /// </summary>
        [SerializeField] private SpawnPlayableStrategyBase m_SpawnPlayableStrategy;

        /// <summary>
        /// Initialize the <see cref="PlayableSpawner"/> components.
        /// </summary>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        /// <param name="assetLoaderService"><see cref="IAssetLoaderService"/></param>
        /// <param name="playableCountMap">Map between playable name and its amount during the level.</param>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/></param>
        public async UniTask<bool> Initialize(IVerticesMapService verticesMapService,
            IAssetLoaderService assetLoaderService, Dictionary<string, int> playableCountMap, IEventAggregator eventAggregator)
        {
            return await m_SpawnPlayableStrategy.Initialize(verticesMapService, assetLoaderService, playableCountMap, eventAggregator);
        }
        /// <summary>
        /// Spawn all playables <see cref="playables"/>
        /// </summary>
        /// <param name="playables">The playables to spawn during gameplay.</param>
        public IEnumerator Spawn(List<PlayableDTO> playables)
        {
            if (playables == null || playables.Count == 0)
            {
                Debug.Log($"Cannot spawn playables when {nameof(playables)} is null empty.");
                yield break;
            }

            if (m_SpawnPlayableStrategy == null)
            {
                Debug.Log($"Cannot spawn playables when {nameof(m_SpawnPlayableStrategy)} is null.");
                yield break;
            }

            foreach (var playable in playables)
            {
                if (playable != null)
                {
                    yield return new WaitForSeconds(playable.Cooldown);
                    Spawn(playable).Forget();
                }
            }
        }
        
        private async UniTaskVoid Spawn(PlayableDTO playableDTO) 
        {
            await m_SpawnPlayableStrategy.Spawn(playableDTO);
        }
    }
}

