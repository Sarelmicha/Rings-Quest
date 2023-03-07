using System;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Token
{
    /// <summary>
    /// Holder Implementation of the <see cref="TokenBase"/>.
    /// </summary>
    public class HolderToken : TokenBase
    {
        [SerializeField] private TrailRenderer m_TrailRenderer;

        private void Awake()
        {
            m_TrailRenderer.time = Single.PositiveInfinity;
        }

        protected override void OnTokenTapBegin()
        {
            m_Speed = 0;
            m_TrailRenderer.time = PlayableDTO.Duration;
        }
    }
}