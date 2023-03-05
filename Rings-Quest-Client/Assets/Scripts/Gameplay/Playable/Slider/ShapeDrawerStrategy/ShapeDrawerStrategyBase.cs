using Happyflow.RingsQuest.Gameplay;
using Happyflow.RingsQuest.Gameplay.Playable.Slider;
using UnityEngine;

/// <summary>
/// Responsible for drawing a shape according to the strategy implementation.
/// </summary>
public abstract class ShapeDrawerStrategyBase : IShapeDrawerStrategy
{
    [SerializeField] protected LineRenderer m_LineRenderer;
    protected int m_NumOfVertices;
    protected IVerticesMapService m_VerticesMapService;

    /// <summary>
    /// Initialize the <see cref="ShapeDrawerStrategyBase"/>.
    /// </summary>
    /// <param name="vertices">The vertices on which the shape will be drawn.</param>
    /// <param name="verticesMapService"><see cref="IVerticesMapService"/> instance.</param>
    public virtual void Initialize(int[] vertices, IVerticesMapService verticesMapService)
    {
        m_NumOfVertices = vertices.Length;
        m_VerticesMapService = verticesMapService;
    }
    
    /// <summary>
    /// Draw a shape according to its implementation 
    /// </summary>
    public abstract void Draw();
}
