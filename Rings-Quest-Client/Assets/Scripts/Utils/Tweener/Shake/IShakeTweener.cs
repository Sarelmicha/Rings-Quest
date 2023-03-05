using UnityEngine;

namespace Happyflow.Utils.Movement 
{
    /// <summary>
    /// Interface for shake tweeners.
    /// </summary>
    public interface IShakeTweener
    {
        /// <summary>
        /// Shake the object.
        /// </summary>
        /// <param name="obj">The transform of the shaking object</param>
        /// <param name="duration">The duration of the shake</param>
        /// <param name="strength">The strength of the shake</param>
        void Shake(Transform obj, float duration, float strength);
    }  
}

