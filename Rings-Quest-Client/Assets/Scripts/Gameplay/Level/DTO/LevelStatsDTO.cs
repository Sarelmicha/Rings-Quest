using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using Newtonsoft.Json;

namespace Happyflow.RingsQuest.Gameplay.Level.DTO 
{
    /// <summary>
    /// Data Transfer Object for a level stats object.
    /// </summary>
    public class LevelStatsDTO 
    {
        /// <summary>
        /// Map between the playable name and the amount it will be played during the gameflow.
        /// </summary>
        [JsonProperty("playables")]
        public PlayableStatsDTO Playables { get; set; }
    }
}

