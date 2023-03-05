using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable
{
    public class PlayableMissedSignal
    {
        /// <summary>
        /// The playable data of the missed playable
        /// </summary>
        public PlayableDTO PlayableDTO { get; }

        public PlayableMissedSignal(PlayableDTO playableDTO)
        {
            PlayableDTO = playableDTO;
        }
    }
}