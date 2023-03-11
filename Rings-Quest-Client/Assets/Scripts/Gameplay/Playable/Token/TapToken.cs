using System;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using Happyflow.RingsQuest.Gameplay.Ring;
using Happyflow.Utils;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Token
{
    /// <summary>
    /// Token base implementation for the <see cref="PlayableBase"/>.
    /// </summary>
    public class TapToken : MonoBehaviour
    {
        [SerializeField] private float m_DistanceToSmash = 0.1f;
        [SerializeField] protected TapDetector m_TapDetector;
        [SerializeField] protected float m_TapDurtation = 0.1f;
        
        protected float m_Speed;
        private float m_TTL;

        private Vector3 m_Direction;
        private Vector3 m_Destination;

        /// <summary>
        /// Invoke when the token is smashed.
        /// </summary>
        public event Action TokenSmashed;
        
        /// <summary>
        /// Invoke when the token is missed.
        /// </summary>
        public event Action TokenMissed;
        
        private bool IsInDestinationRadius => Vector2.Distance(gameObject.transform.position, m_Destination) < m_DistanceToSmash;

        /// <summary>
        /// Call to spawn a playable;
        /// </summary>
        /// <param name="playableDTO">The playableDTO of the token to spawn.</param>
        /// <param name="destination">The destination of the token.</param>
        public virtual void Spawn(PlayableDTO playableDTO, Vector3 destination)
        {
            // Calculate the required velocity
            var position = transform.position;
            m_Destination = destination;

            m_TTL = playableDTO.TTL;
            m_Speed = Vector3.Distance(position, m_Destination) / m_TTL;
            m_Direction = (m_Destination - position).normalized;
            m_TapDetector.TapDuration = m_TapDurtation;
        }

        private void Start()
        {
            SubscribeListeners();
        }

        private void OnDestroy()
        {
            UnsubscribeListeners();
        }

        private void FixedUpdate()
        {
            transform.position += m_Direction * m_Speed * Time.fixedDeltaTime;
        }
        
        private void OnTapBegin()
        {
            if (IsInDestinationRadius)
            {
                OnTokenTapBegin();
            }
        }
        
        private void OnTapEnd(bool wasSuccessful)
        {
            if (IsInDestinationRadius && wasSuccessful)
            {
                TokenSmashed?.Invoke();
                return;
            }
            
            TokenMissed?.Invoke();
        }
        
        private void SubscribeListeners()
        {
            m_TapDetector.TapBegin += OnTapBegin;
            m_TapDetector.TapEnd += OnTapEnd;
        }
        
        private void UnsubscribeListeners()
        {
            m_TapDetector.TapBegin -= OnTapBegin;
            m_TapDetector.TapEnd -= OnTapEnd;
        }

        protected virtual void OnTokenTapBegin()
        {
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<RingManager>() == null)
            {
                return;
            }

            TokenMissed?.Invoke();
        }
    }
}