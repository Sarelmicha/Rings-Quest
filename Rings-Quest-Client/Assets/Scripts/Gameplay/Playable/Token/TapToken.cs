using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Token
{
    /// <summary>
    /// Tap Implementation of the <see cref="TokenBase"/>.
    /// </summary>
    public class TapToken : TokenBase
    {
        private void FixedUpdate()
        {
            transform.position += m_Direction * m_Speed * Time.fixedDeltaTime;
        }

        protected override void OnTokenClick()
        {
            Smash();
        }
    }
}