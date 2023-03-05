using System;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Slider
{
    /// <summary>
    /// Responsible for detect when the user drag its finger inside a <see cref="LineRenderer"/> and act accordingly. 
    /// </summary>
    public class DragDetector : MonoBehaviour
    { 
        [SerializeField] private LineRenderer m_LineRenderer;
        [SerializeField]private float m_DistanceToDetected = 0.1f;
        private bool m_Dragging = false;
        private Camera m_Camera;
            
        public event Action DragFromStartToEndPointSuccessfully;
        public event Action DragStarted;

        private void Start()
        {
            m_Camera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount <= 0)
            {
                return;
            }
            
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Convert the touch position to world space
                Vector3 touchPosWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));

                // Check if the touch position is over the first vertex of the line renderer
                if (IsTouchOverVertex(touchPosWorldSpace, 0))
                {
                    m_Dragging = true;
                    DragStarted?.Invoke();
                }
            }
            else if (touch.phase == TouchPhase.Moved && m_Dragging)
            {
                // Check if the touch is currently over the shape
                if (IsTouchOverShape(touch.position))
                {
                    // Track the touch's position
                }
            }
            else if (touch.phase == TouchPhase.Ended && m_Dragging)
            {
                // Check if the touch ended over the shape
                if (IsTouchOverShape(touch.position))
                {
                    // The user has dragged their finger from the starting point to the end point on the shape
                    DragFromStartToEndPointSuccessfully?.Invoke();
                }

                m_Dragging = false;
            }
        }

        private bool IsTouchOverShape(Vector2 touchPos)
        {
            // Convert the touch position to world space
            Vector3 touchPosWorldSpace = m_Camera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane));

            // Check if the touch position is over the line renderer
            RaycastHit2D hit = Physics2D.Raycast(touchPosWorldSpace, Vector2.zero);
            
            return hit.collider != null && hit.collider.gameObject == gameObject;
        }

        private bool IsTouchOverVertex(Vector3 touchPos, int vertexIndex)
        {
            // Check if the touch position is within a certain distance of the vertex
            return Vector3.Distance(touchPos, m_LineRenderer.GetPosition(vertexIndex)) <= m_DistanceToDetected;
        }
    }
}

