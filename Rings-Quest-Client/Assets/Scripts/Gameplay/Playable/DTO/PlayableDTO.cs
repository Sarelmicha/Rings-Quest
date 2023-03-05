namespace Happyflow.RingsQuest.Gameplay.Playable.DTO 
{
    /// <summary>
    /// Data Transfer Object for a Playable object.
    /// </summary>
    public class PlayableDTO 
    {
        /// <summary>
        /// Enumeration representing the type of playable.
        /// </summary>
        public PlayableType PlayableType { get; set; }

        /// <summary>
        /// The name of the playable.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The cooldown of the playable.
        /// </summary>
        public float Cooldown { get; set; }
        
        /// <summary>
        /// The duration of the playable.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// The vertices that the playable should interact with.
        /// </summary>
        public int[] Vertices { get; set; }
        
        /// <summary>
        /// The score of the playable.
        /// </summary>
        public int Score { get; set; }
    }
}


