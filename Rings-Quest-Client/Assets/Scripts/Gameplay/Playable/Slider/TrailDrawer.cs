using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Slider
{
    /// <summary>
    /// Responsible for drawing a trail according the user's tap
    /// </summary>
    public class TrailDrawer : MonoBehaviour
    {
        [SerializeField] private TrailRenderer m_TrailRenderer;
        private Camera m_Camera;
        
        /// <summary>
        /// A flag that indicate whether it can draw a trail or not.
        /// </summary>
        public bool CanTrail { get; set; }

        private void Start()
        {
            m_Camera = Camera.main;
        }

        private void Update()
        {
            // Check if the user has touched the screen or it can show trail
            if (Input.touchCount <= 0|| !CanTrail)
            {
                return;
            }
            
            Touch touch = Input.GetTouch(0);

            // Check if the user has started dragging
            if (touch.phase == TouchPhase.Began)
            {
                // Start the trail renderer
                m_TrailRenderer.Clear();
                m_TrailRenderer.emitting = true;
            }
            // Check if the user is dragging
            else if (touch.phase == TouchPhase.Moved)
            {
                // Get the current touch position
                Vector3 currentMousePosition = touch.position;
                
                // Convert the screen touch position to a world position
                Vector3 worldPos = m_Camera.ScreenToWorldPoint(
                    new Vector3(currentMousePosition.x, currentMousePosition.y, m_Camera.nearClipPlane));

                // Set the position of the trail renderer to the world position
                m_TrailRenderer.transform.position = worldPos;
            }
            // Check if the user has stopped dragging
            else if (touch.phase == TouchPhase.Ended)
            {
                // Stop the trail renderer
                m_TrailRenderer.emitting = false;
                CanTrail = false;
            }
        }
    }
}