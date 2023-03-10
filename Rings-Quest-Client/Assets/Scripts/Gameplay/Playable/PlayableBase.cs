using System;
using System.IO;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using Happyflow.RingsQuest.Gameplay.Ring;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable 
{
    /// <summary>
    /// Abstract base class for playable objects.
    /// </summary>
    public abstract class PlayableBase : MonoBehaviour
    {
        /// <summary>
        /// The Data Transfer Object for the playable object.
        /// </summary>
        public PlayableDTO PlayableDTO
        {
            get => m_PlayableDTO;
            set
            {
                if (!IsDataValid(value))
                {
                    Debug.Log($"Cannot set {nameof(PlayableDTO) } with an invalid data.");
                    return;
                }

                m_PlayableDTO = value;
            }
        }
        
        /// <summary>
        /// Invoked when the playable is smashed.
        /// </summary>
        public event Action<PlayableBase> OnSmash;

        /// <summary>
        /// Invoked when the playable is missed. 
        /// </summary>
        public event Action<PlayableBase> OnMiss;

        protected IVerticesMapService m_VerticesMapService;
        protected PlayableStatus m_PlayableStatus;
        private PlayableDTO m_PlayableDTO;
        
        /// <summary>
        /// Initialize the playable 
        /// </summary>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        public virtual void Initialize(IVerticesMapService verticesMapService)
        {
            m_VerticesMapService = verticesMapService;
            gameObject.SetActive(false);
            m_PlayableStatus = new PlayableStatus();
        }

        /// <summary>
        /// Call to spawn a playable;
        /// </summary>
        /// <param name="playableDTO">The playableDTO of the playable to spawn</param>
        public virtual void Spawn(PlayableDTO playableDTO)
        {
            if (!IsDataValid(playableDTO))
            {
                throw new InvalidDataException();
            }

            PlayableDTO = playableDTO;
            gameObject.SetActive(true);   
        }
        
        /// <summary>
        /// Validate that the required data that was received is valid. 
        /// </summary>
        /// <returns>True if the data is valid, otherwise return False.</returns>
        protected virtual bool IsDataValid(PlayableDTO playableBase)
        {
            if (playableBase.Vertices == null || playableBase.Vertices.Length == 0)
            {
                Debug.Log($"{nameof(Token)} cannot initialize when Vertices data is not valid.");
                return false; 
            }

            return true;
        }

        /// <summary>
        /// Method to be executed when the playable object is smashed.
        /// </summary>
        protected virtual void Smash()
        {
            if (m_PlayableStatus != PlayableStatus.Idle)
            {
                return;
            }
            m_PlayableStatus = PlayableStatus.Smashed;
            ResetState();
            OnSmash?.Invoke(this);
        }

        protected virtual void Miss()
        {
            if (m_PlayableStatus != PlayableStatus.Idle)
            {
                return;
            }
            
            m_PlayableStatus = PlayableStatus.Missed;
            ResetState();
            OnMiss?.Invoke(this);
        }

        protected virtual void ResetState()
        {
            m_PlayableStatus = PlayableStatus.Idle;
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<RingManager>() == null)
            {
                return;
            }

            Miss();
        }
    }
}
