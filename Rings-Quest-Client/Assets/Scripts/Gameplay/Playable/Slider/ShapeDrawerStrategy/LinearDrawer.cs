namespace Happyflow.RingsQuest.Gameplay.Playable.Slider
{
    /// <summary>
    /// Implementation of the <see cref="ShapeDrawerStrategyBase"/>.
    /// Responsible for draw a linear shape.
    /// </summary>
    public class LinearDrawer : ShapeDrawerStrategyBase
    {
        /// <summary>
        /// Initialize the <see cref="ShapeDrawerStrategyBase"/>.
        /// </summary>
        /// <param name="vertices">The vertices on which the shape will be drawn.</param>
        /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
        public override void Initialize(int[] vertices, IVerticesMapService verticesMapService)
        {
            base.Initialize(vertices, verticesMapService);
            m_LineRenderer.useWorldSpace = false;
            m_LineRenderer.positionCount = m_NumOfVertices;
        }
        
        /// <summary>
        /// Draw a linear shape.
        /// </summary>
        public override void Draw()
        {
            for (var i = 0; i < m_LineRenderer.positionCount; i++)
            {
                m_LineRenderer.SetPosition(i, m_VerticesMapService.GetVertexTransform(i));
            }
        }
    }
}