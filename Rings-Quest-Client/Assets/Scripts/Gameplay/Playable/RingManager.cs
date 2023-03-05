using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Happyflow.Core.AssetLoader;
using Happyflow.Core.EventAggregator;
using Happyflow.Core.ServiceLocator;
using Happyflow.RingsQuest.Gameplay.Playable;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Ring
{
    /// <summary>
    /// The RingManager manages the spawning of playables in a the ring.
    /// </summary>
    public class RingManager : MonoBehaviour
    {
        /// <summary>
        /// The spawner for the playables.
        /// </summary>
        [SerializeField] private PlayableSpawner m_PlayableSpawner;

        /// <summary>
        /// The vertices of the ring.
        /// </summary>
        [SerializeField] private List<Vertex> m_Vetrices;

        /// <summary>
        /// The playables to be spawned.
        /// </summary>
        private List<PlayableDTO> m_Playables;
        
        /// <summary>
        /// Initialize the <see cref="RingManager"/>. 
        /// </summary>
        /// <param name="playables">The playables of the gameplay.</param>
        /// <param name="playableCountMap">Map between playable name and its amount during the level.</param>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/></param>
        /// <returns>True if Initialized properly, otherwise returns False.</returns>
        public async UniTask<bool> Initialize(List<PlayableDTO> playables, Dictionary<string, int> playableCountMap, IEventAggregator eventAggregator)
        {
            if (playables == null || playableCountMap == null || eventAggregator == null)
            {
                Debug.Log($"Cannot initialize {nameof(RingManager)} with an invalid data.");
                return false;
            }

            m_Playables = playables;
            IVerticesMapService verticesMapService = new VerticesMapService(m_Vetrices);
            return await m_PlayableSpawner.Initialize(verticesMapService, ServiceLocator.Instance.Get<IAssetLoaderService>(), playableCountMap, eventAggregator);
        }

        /// <summary>
        /// Start spawning the playables.
        /// </summary>
        public void Spawn()
        {
            StartCoroutine(m_PlayableSpawner.Spawn(m_Playables));
        }
    }
}

