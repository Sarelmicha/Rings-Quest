using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Happyflow.Core.AssetLoader;
using Happyflow.Core.EventAggregator;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable
{
    /// <summary>
    /// Interface for spawn playable strategy.
    /// </summary>
    public interface ISpawnPlayableStrategy : IDisposable
    {
        /// <summary>
        /// Initialize the spawn playable strategy
        /// </summary>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        /// <param name="assetLoaderService"><see cref="AssetLoaderService"/> instance.</param>
        /// <param name="playablesCountMap">Map the playables by name and count for a specific gameplay</param>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/></param>
        /// <returns>True if Initialized properly, otherwise returns False.</returns>
        UniTask<bool> Initialize(IVerticesMapService verticesMapService, IAssetLoaderService assetLoaderService, Dictionary<string, int> playablesCountMap, IEventAggregator eventAggregator);

        /// <summary>
        /// Spawn the playables according to the strategy implementation
        /// </summary>
        UniTask Spawn(PlayableDTO playableDTO);
    } 
}

