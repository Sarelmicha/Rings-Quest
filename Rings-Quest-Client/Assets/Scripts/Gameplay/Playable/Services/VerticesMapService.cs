using System.Collections.Generic;
using Happyflow.RingsQuest.Gameplay.Ring;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay
{
    public class VerticesMapService : IVerticesMapService
    {
        private readonly Dictionary<int, Vector2> m_VerticesMap;

        /// <summary>
        /// Initializes a new instance of the VerticesMapService class. 
        /// </summary>
        /// <param name="vertices">All the vertices that play</param>
        public VerticesMapService(IReadOnlyList<Vertex> vertices)
        {
            m_VerticesMap = ConvertToMap(vertices);
        }

        /// <summary>
        /// Map between a vertex index to its position.
        /// </summary>
        /// <param name="index">The index of vertex</param>
        /// <returns>The vertex transform if index exists, otherwise returns <see cref="Vector2.zero"/>.</returns>
        public Vector2 GetVertexTransform(int index)
        {
            if (index < 0 || index >= m_VerticesMap.Count || !m_VerticesMap.ContainsKey(index))
            {
                Debug.Log("received index is invalid.");
                return Vector2.zero;
            }

            return m_VerticesMap[index];
        }
        
        /// <summary>
        /// Converts a list of vertex to a dictionary that maps a vertex index to its position in 2D space.
        /// </summary>
        /// <param name="vertices">The list of GameObjects to convert.</param>
        /// <returns>A dictionary that maps a vertex index to its position in 2D space.</returns>
        private Dictionary<int, Vector2> ConvertToMap(IReadOnlyList<Vertex> vertices)
        {
            var verticesMap = new Dictionary<int, Vector2>();
            
            for (var i = 0; i < vertices.Count; i++)
            {
                verticesMap[i] = vertices[i].transform.position;
            }
            
            return verticesMap;
        }
    }
}

