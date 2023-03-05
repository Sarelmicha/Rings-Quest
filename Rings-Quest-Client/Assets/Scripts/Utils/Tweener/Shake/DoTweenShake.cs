using DG.Tweening;
using UnityEngine;

namespace Happyflow.Utils.Movement
{
    /// <summary>
    /// Shaker class using the DoTween library to handle shaking tweening.
    /// </summary>
    public class DoTweenShake : IShakeTweener
    {
        /// <summary>
        /// Shake the object.
        /// </summary>
        /// <param name="obj">The transform of the shaking object</param>
        /// <param name="duration">The duration of the shake</param>
        /// <param name="strength">The strength of the shake</param>
        public void Shake(Transform obj, float duration, float strength)
        {
            obj.DOShakePosition(duration, strength);
        }
    }
}

