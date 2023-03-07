using System;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using Happyflow.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Happyflow.RingsQuest.Gameplay.Playable.Token
{
    /// <summary>
    /// Token base implementation for the <see cref="PlayableBase"/>.
    /// </summary>
    public class TokenBase : PlayableBase
    {
        [SerializeField] private float m_DistanceToSmash = 0.1f;
        [SerializeField] private TapDetector m_TapDetector;
        
        protected float m_Speed;
        private Vector3 m_Direction;
        
        private Vector3 Destination => m_VerticesMapService.GetVertexTransform(PlayableDTO.Vertices[0]);
        private bool IsInDestinationRadius => Vector2.Distance(gameObject.transform.position, Destination) < m_DistanceToSmash;

        private void Start()
        {
            SubscribeListeners();
        }

        private void OnDestroy()
        {
            UnsubscribeListeners();
        }

        /// <summary>
        /// Call to spawn a playable;
        /// </summary>
        /// <param name="playableDTO">The playableDTO of the playable to spawn</param>
        public override void Spawn(PlayableDTO playableDTO)
        {
            base.Spawn(playableDTO);
            
            // Calculate the required velocity
            var position = transform.position;
            
            m_Speed = Vector3.Distance(position, Destination) / playableDTO.Duration;
            m_Direction = (Destination - position).normalized;
        }
        
        private void FixedUpdate()
        {
            transform.position += m_Direction * m_Speed * Time.fixedDeltaTime;
        }
        
        /// <summary>
        /// Method to be executed when the token is smashed.
        /// </summary>
        protected override void Smash()
        {
            //Call some animation of smash
            base.Smash();
        }

        protected override void Miss()
        {
            //Call some animation of miss
            base.Miss();
        }
        
        protected override void ResetState()
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
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
                Smash();
                return;
            }
            
            Miss();
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
    }
}