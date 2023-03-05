using Happyflow.Core.ServiceLocator;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay
{
    /// <summary>
    /// interface for service which manage the vertices maps.
    /// </summary>
    public interface IVerticesMapService
    {
        /// <summary>
        /// Map between a vertex index to its position.
        /// </summary>
        /// <param name="index">The index of vertex</param>
        /// <returns>The vertex transform if index exists, otherwise returns <see cref="Vector2.zero"/>.</returns>
        Vector2 GetVertexTransform(int index);
    }
}
