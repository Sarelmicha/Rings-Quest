using System;
using System.Collections;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Slider
{
    /// <summary>
    /// Slicer implementation for the <see cref="PlayableBase"/>
    /// </summary>
    public class Slider : PlayableBase
    {
        [SerializeField] private ShapeDrawerStrategyBase m_ShapeDrawerStrategy;
        [SerializeField] private DragDetector m_DragDetector;
        [SerializeField] private TrailDrawer m_TrailDrawer;
        
        /// <summary>
        /// Initialize the playable 
        /// </summary>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        public override void Initialize(IVerticesMapService verticesMapService)
        {
            base.Initialize(verticesMapService);
            m_DragDetector.DragFromStartToEndPointSuccessfully += Smash;
            m_DragDetector.DragStarted += OnDragStarted;
        }

        /// <summary>
        /// Call to spawn a playable;
        /// </summary>
        /// <param name="playableDTO">The playableDTO of the playable to spawn</param>
        public override void Spawn(PlayableDTO playableDTO)
        {
            base.Spawn(playableDTO);
            StartCoroutine(Draw());
        }

        private void OnDestroy()
        {
            m_DragDetector.DragFromStartToEndPointSuccessfully -= Smash;
            m_DragDetector.DragStarted -= OnDragStarted;
        }

        private IEnumerator Draw()
        {
            m_ShapeDrawerStrategy.Draw();
            yield return new WaitForSeconds(PlayableDTO.TTL);
            ResetState();
        }
        private void OnDragStarted()
        {
            EnableTrail(false);
        }

        /// <summary>
        /// Method to be executed when the slider is smashed.
        /// </summary>
        protected override void Smash()
        {
            // Call some smash animation
            base.Smash();
        }
        
        /// <summary>
        /// Method to be executed when the slider is missed.
        /// </summary>
        protected override void Miss()
        {
            // Call some miss animation
            base.Miss();
        }

        protected override void ResetState()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }

        private void EnableTrail(bool enable)
        {
            m_TrailDrawer.CanTrail = enable;
        }
    }
}