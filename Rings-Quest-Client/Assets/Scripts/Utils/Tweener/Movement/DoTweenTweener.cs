using System;
using DG.Tweening;
using UnityEngine;

namespace Happyflow.Utils.Movement 
{
    /// <summary>
    /// Tweener class using the DoTween library to handle movement tweening.
    /// </summary>
    public class DoTweenTweener : IMovementTweener
    {
        private Sequence m_CurrentSequence;
        
        /// <summary>
        /// Move the transform to a specific position 
        /// </summary>
        /// <param name="obj">The transform of the moving object.</param>
        /// <param name="targetPosition">The position to move to.</param>
        /// <param name="duration">The duration of the movement.</param>
        /// <param name="movementSwing">Before the actual movement, movement swing can be added (including burst and duration of the swing).</param>
        /// <param name="onComplete">Call when movement is finished.</param>
        public void MoveTo(Transform obj, Vector3 targetPosition, float duration, MovementSwing movementSwing = null, Action onComplete = null)
        {
            m_CurrentSequence = DOTween.Sequence();

            if (movementSwing != null)
            {
                m_CurrentSequence.Append(obj.DOMove((obj.position - targetPosition).normalized * movementSwing.Burst, movementSwing.Duration));
            }

            m_CurrentSequence.Append(obj.DOMove(targetPosition, duration));
            m_CurrentSequence.OnComplete(() => onComplete?.Invoke());
            m_CurrentSequence.Play();
        }

        /// <summary>
        /// Scale the transform to a specific scale. 
        /// </summary>
        /// <param name="obj">The transform of the moving object.</param>
        /// <param name="targetScale">The scale to scale to.</param>
        /// <param name="duration">The duration of the scale.</param>
        /// <param name="onComplete">Call when scale is finished.</param>
        public void Scale(Transform obj, Vector3 targetScale, float duration, Action onComplete = null)
        {
            m_CurrentSequence = DOTween.Sequence();
            m_CurrentSequence.Append(obj.DOScale(targetScale, duration));
            m_CurrentSequence.OnComplete(() => onComplete?.Invoke());
            m_CurrentSequence.Play();
        }

        /// <summary>
        /// Stop the current tween
        /// </summary>
        public void StopTween()
        {
            m_CurrentSequence?.Kill();
        }
    }
}
