using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Happyflow.RingsQuest.Gameplay.Playable.Token
{
    /// <summary>
    /// Token base implementation for the <see cref="PlayableBase"/>.
    /// </summary>
    public abstract class TokenBase : PlayableBase, IPointerClickHandler
    {
        [SerializeField] private float m_DistanceToSmash = 0.1f;
        
        protected float m_Speed;
        protected Vector3 m_Direction;
        
        private Vector3 Destination => m_VerticesMapService.GetVertexTransform(PlayableDTO.Vertices[0]);
        private bool IsInDestinationRadius => Vector2.Distance(gameObject.transform.position, Destination) < m_DistanceToSmash;
        
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
        
        /// <summary>
        /// Called when the token was clicked
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsInDestinationRadius)
            {
                OnTokenClick();
            }
        }
        
        protected abstract void OnTokenClick();
    }
}