using System;
using UnityEngine;

namespace Happyflow.Utils.Movement 
{
    /// <summary>
    /// Interface for movement tweeners.
    /// </summary>
    public interface IMovementTweener
    {
        /// <summary>
        /// Move the transform to a specific position 
        /// </summary>
        /// <param name="obj">The transform of the moving object</param>
        /// <param name="targetPosition">The position to move to</param>
        /// <param name="duration">The duration of the movement</param>
        /// <param name="movementSwing">Before the actual movement, movement swing can be added (including burst and duration of the swing)</param>
        /// <param name="onComplete">Call when movement is finished</param>
        void MoveTo(Transform obj, Vector3 targetPosition, float duration, MovementSwing movementSwing = null,
            Action onComplete = null);

        /// <summary>
        /// Stop the tween
        /// </summary>
        void StopTween();
    }
}