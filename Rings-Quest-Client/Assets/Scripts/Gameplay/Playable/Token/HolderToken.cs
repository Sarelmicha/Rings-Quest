using System;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Token
{
    /// <summary>
    /// Holder Implementation of the <see cref="TapToken"/>.
    /// </summary>
    public class HolderToken : TapToken
    {
        [SerializeField] private TrailRenderer m_TrailRenderer;

        private void Awake()
        {
            m_TrailRenderer.time = Single.PositiveInfinity;
        }

        /// <summary>
        /// Call to spawn a playable;
        /// </summary>
        /// <param name="playableDTO">The playableDTO of the token to spawn.</param>
        /// <param name="destination">The destination of the token.</param>
        /// <param name="initialDistanceFactor"></param>
        public override void Spawn(PlayableDTO playableDTO, Vector3 destination, float initialDistanceFactor)
        {
            base.Spawn(playableDTO, destination, initialDistanceFactor);
            m_TapDurtation = playableDTO.InteractableTime;
        }
        
        protected override void OnTokenTapBegin()
        {
            m_Speed = 0;
            m_TrailRenderer.time = m_TapDurtation;
            m_TapDetector.TapDuration = m_TapDurtation;
        }
    }
}