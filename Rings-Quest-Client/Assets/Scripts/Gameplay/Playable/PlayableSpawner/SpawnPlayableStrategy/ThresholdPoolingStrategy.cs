using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Happyflow.Core.AssetLoader;
using Happyflow.Core.EventAggregator;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable
{
    /// <summary>
    /// Handle the strategy of spawning Playables using threshold pooling strategy.
    /// </summary>
    public class ThresholdPoolingStrategy : SpawnPlayableStrategyBase
    {
        [Header("The threshold per pool queue.")] [SerializeField] private int m_Threshold;
        private Dictionary<string, Queue<PlayableBase>> m_PlayablesPool;

        /// <summary>
        /// /// Initialize the spawn playable strategy
        /// </summary>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        /// <param name="assetLoaderService"><see cref="AssetLoaderService"/> instance.</param>
        /// <param name="playablesCountMap">Map the playables by name and count for a specific gameplay</param>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/></param>
        /// <returns>True if Initialized properly, otherwise returns False.</returns>
        public override async UniTask<bool> Initialize(IVerticesMapService verticesMapService, IAssetLoaderService assetLoaderService, Dictionary<string, int> playablesCountMap, IEventAggregator eventAggregator)
        {
            var initialized = await base.Initialize(verticesMapService, assetLoaderService, playablesCountMap, eventAggregator);

            if (!initialized)
            {
                return false;
            }

            await InitializeInternal();

            return true;
        }

        /// <summary>
        /// Spawn a single <see cref="Playable"/>
        /// </summary>
        /// <param name="playableDTO">The playableDTO of the playable to spawn</param>
        public override async UniTask Spawn(PlayableDTO playableDTO)
        {
            if (!m_PlayablesPool.ContainsKey(playableDTO.Name))
            {
                Debug.Log($"A playable {playableDTO.Name} is not exists in the pool.");
                return;
            }
            
            PlayableBase playable = m_PlayablesPool[playableDTO.Name].Count == 0
                ? await InstantiatePlayable(playableDTO.Name)
                : m_PlayablesPool[playableDTO.Name].Dequeue();
            try
            {
                playable.Spawn(playableDTO);
            }
            catch (InvalidDataException)
            {
                Debug.Log($"Tried to spawn a playable with an invalid {nameof(playableDTO)} : {playableDTO}");
            }
        }

        private async UniTask<PlayableBase> InstantiatePlayable(string playableName)
        {
            var playableAsset = await m_AssetLoaderService.LoadAssetAsync<GameObject>(playableName);

            if (playableAsset == null)
            {
                Debug.Log($"Cannot load the prefab {playableName}.");
                return null;
            }
            
            var playablePrefab = Instantiate(playableAsset, transform);

            var playable = playablePrefab.GetComponent<PlayableBase>();

            if (playable == null)
            {
                Debug.Log($"{playable} does not contains the component {nameof(PlayableBase)}");
                return null;
            }

            playable.Initialize(m_VerticesMapService);
            playable.OnSmash += OnPlayableSmashed;
            playable.OnMiss += OnPlayableMissed;
            return playable;
        }

        private void OnPlayableMissed(PlayableBase missedPlayable)
        {
            if (!TryAddToPool(missedPlayable))
            {
                return;
            }
            
            m_EventAggregator.Fire(new PlayableMissedSignal(missedPlayable.PlayableDTO));
        }

        private void OnPlayableSmashed(PlayableBase smashedPlayable)
        {
            if (!TryAddToPool(smashedPlayable))
            {
                return;
            }
            
            m_EventAggregator.Fire(new PlayableSmashedSignal(smashedPlayable.PlayableDTO));
        }

        private bool TryAddToPool(PlayableBase smashedPlayable)
        {
            if (!m_PlayablesPool.ContainsKey(smashedPlayable.PlayableDTO.Name))
            {
                Debug.Log($"{nameof(smashedPlayable.PlayableDTO.Name)} was smashed but there is no queue for that playable.");
                return false;
            }

            m_PlayablesPool[smashedPlayable.PlayableDTO.Name].Enqueue(smashedPlayable);
            return true;
        }

        /// <summary>
        /// Dispose all of the components of the <see cref="ThresholdPoolingStrategy"/>
        /// </summary>
        public override void Dispose()
        {
            foreach (var playable in m_PlayablesPool.SelectMany(keyValue => keyValue.Value))
            {
                m_AssetLoaderService.UnloadAsset<PlayableBase>(playable.PlayableDTO.Name);
                playable.OnSmash -= OnPlayableSmashed;
                playable.OnMiss -= OnPlayableMissed;
            }
        }
        
        private async UniTask InitializeInternal()
        {
            m_PlayablesPool = new Dictionary<string, Queue<PlayableBase>>();

            foreach (var playablesCount in m_PlayablesCountMap)
            {
                var playableThreshold = Math.Min(playablesCount.Value, m_Threshold);

                m_PlayablesPool[playablesCount.Key] = new Queue<PlayableBase>(playableThreshold);

                for (var i = 0; i < playableThreshold; i++)
                {
                    var playable = await InstantiatePlayable(playablesCount.Key);

                    if (playable != null)
                    {
                        m_PlayablesPool[playablesCount.Key].Enqueue(playable);
                    }
                    else
                    {
                        Debug.Log($"Cannot instantiate playable with playable key {playablesCount.Key}.");
                    }
                }
            }
        }
    }
}