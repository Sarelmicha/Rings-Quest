using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Slider
{
    /// <summary>
    /// Implementation of the <see cref="ShapeDrawerStrategyBase"/>.
    /// Responsible for draw a circular shape.
    /// </summary>
    public class CircularDrawer : ShapeDrawerStrategyBase
    {
        private float m_Radius = 2.0f;

        /// <summary>
        /// Initialize the <see cref="ShapeDrawerStrategyBase"/>.
        /// </summary>
        /// <param name="vertices">The vertices on which the shape will be drawn.</param>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        public override void Initialize(int[] vertices, IVerticesMapService verticesMapService)
        {
            base.Initialize(vertices, verticesMapService);
            m_LineRenderer.useWorldSpace = false;
            m_LineRenderer.positionCount = m_NumOfVertices + 1;
        }

        /// <summary>
        /// Draw a circular shape.
        /// </summary>
        public override void Draw()
        {
            float x;
            float y;
            float z = 0f;

            float angle = 20f;

            for (var i = 0; i < m_NumOfVertices + 1; i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * m_Radius;
                y = Mathf.Cos(Mathf.Deg2Rad * angle) * m_Radius;

                m_LineRenderer.SetPosition(i, new Vector3(x, y, z));

                angle += 360f / m_NumOfVertices;
            }
        }
    }
}