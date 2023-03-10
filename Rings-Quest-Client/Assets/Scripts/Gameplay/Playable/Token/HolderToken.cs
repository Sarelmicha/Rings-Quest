using System;
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

        protected override void OnTokenTapBegin()
        {
            m_Speed = 0;
            m_TrailRenderer.time = m_Duration;
            m_TapDetector.TapDuration = m_Duration;
        }
    }
}