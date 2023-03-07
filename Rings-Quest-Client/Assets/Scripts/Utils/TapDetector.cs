using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Happyflow.Utils
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Detects when the user has performed a tap on an object.
    /// </summary>
    public class TapDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool m_IsTapping;
        private float m_TapStartTime;
        
        /// <summary>
        /// Event raised when the user begins tapping on the object.
        /// </summary>
        public event Action TapBegin;

        /// <summary>
        ///  Event raised when the user releases their finger from the object.
        /// </summary>
        public event Action<bool> TapEnd;

        [Header("The duration in seconds that the user must tap on the object before a long tap is detected.")]
        [SerializeField] private float m_TapDuration = 1.0f;

        /// <summary>
        /// Called when the user presses down on the object.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            m_IsTapping = true;
            m_TapStartTime = Time.time;
            TapBegin?.Invoke();
        }

        /// <summary>
        /// Called when the user releases their finger from the object.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            m_IsTapping = false;
            float tapDuration = Time.time - m_TapStartTime;
            bool wasSuccessful = tapDuration >= m_TapDuration;

            TapEnd?.Invoke(wasSuccessful);
        }

        /// <summary>
        /// Called every frame while the user is tapping on the object.
        /// </summary>
        private void Update()
        {
            if (!m_IsTapping)
            {
                return;
            }
            
            var tapDuration = Time.time - m_TapStartTime;
                
            if (tapDuration >= m_TapDuration)
            {
                OnPointerUp(null);
            }
        }
    }
}
