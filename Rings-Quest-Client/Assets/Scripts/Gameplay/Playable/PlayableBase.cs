using System;
using System.Collections.Generic;
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
        private PlayableDTO m_PlayableDTO;
        
        /// <summary>
        /// Initialize the playable 
        /// </summary>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        public virtual void Initialize(IVerticesMapService verticesMapService)
        {
            m_VerticesMapService = verticesMapService;
            gameObject.SetActive(false);
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
        private bool IsDataValid(PlayableDTO playableBase)
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
            ResetState();
            OnSmash?.Invoke(this);
        }

        protected virtual void Miss()
        {
            ResetState();
            OnMiss?.Invoke(this);
        }

        protected abstract void ResetState();

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
