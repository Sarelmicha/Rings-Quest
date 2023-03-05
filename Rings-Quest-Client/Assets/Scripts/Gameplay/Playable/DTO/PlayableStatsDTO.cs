using System.Collections.Generic;
using Newtonsoft.Json;

namespace Happyflow.RingsQuest.Gameplay.Playable.DTO
{
    /// <summary>
    /// Data Transfer Object for a playable stats object.
    /// </summary>
    public class PlayableStatsDTO
    {
        [JsonProperty("countByName")]
        public Dictionary<string, int> PlayablesCountMap { get; set; }
        
        /// <summary>
        /// The total amount of playables in the level.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}


