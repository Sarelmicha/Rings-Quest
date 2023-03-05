using System.Collections.Generic;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using Newtonsoft.Json;

namespace Happyflow.RingsQuest.Gameplay.Level.DTO
{
    /// <summary>
    /// Data Transfer Object for a Level object.
    /// </summary>
    public class LevelDTO
    {
        /// <summary>
        /// The id of the level
        /// </summary>
        [JsonProperty("id")]
        public int LevelId { get; set; }

        /// <summary>
        /// All the playables that will be played during the level.
        /// </summary>
        [JsonProperty("playables")]
        public List<PlayableDTO> Playables { get; set; }
        
        /// <summary>
        /// The stats of all the playables that should be spawned during the level.
        /// </summary>
        [JsonProperty("stats")]
        public LevelStatsDTO LevelStatsDTO { get; set; }
    }
}

