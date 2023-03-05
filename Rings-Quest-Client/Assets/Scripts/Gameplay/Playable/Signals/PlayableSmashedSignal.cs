using Happyflow.RingsQuest.Gameplay.Playable.DTO;

namespace Happyflow.RingsQuest.Gameplay.Playable
{
    /// <summary>
    /// A signal that notify that a playable was smashed.
    /// </summary>
    public class PlayableSmashedSignal
    {
        /// <summary>
        /// The playable data of the smashed playable
        /// </summary>
        public PlayableDTO PlayableDTO { get; }

        public PlayableSmashedSignal(PlayableDTO playableDTO)
        {
            PlayableDTO = playableDTO;
        }
    }
}

