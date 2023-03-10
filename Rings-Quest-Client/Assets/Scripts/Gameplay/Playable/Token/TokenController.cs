using System.Collections.Generic;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay.Playable.Token
{
    public class TokenController : PlayableBase
    {
        [SerializeField] private List<TapToken> m_Tokens;
        private int m_NumOfSmashedTokens;
        
        /// <summary>
        /// Call to spawn a playable;
        /// </summary>
        /// <param name="playableDTO">The playableDTO of the playable to spawn</param>
        public override void Spawn(PlayableDTO playableDTO)
        {
            base.Spawn(playableDTO);

            for (var i = 0; i < PlayableDTO.Vertices.Length; i++)
            {
                SubscribeTokenListeners(m_Tokens[i]);
                SpawnToken(m_Tokens[i], m_VerticesMapService.GetVertexTransform(PlayableDTO.Vertices[i]));
            }
        }

        private void SpawnToken(TapToken token, Vector2 destination)
        {
            token.Spawn(destination, PlayableDTO.Duration);
        }

        private void SubscribeTokenListeners(TapToken token)
        {
            token.TokenSmashed += OnTokenSmashed;
            token.TokenMissed += OnTokenMissed;
        }
        
        private void UnsubscribeTokenListeners(TapToken token)
        {
            token.TokenSmashed -= OnTokenSmashed;
            token.TokenMissed -= OnTokenMissed;
        }

        private void OnTokenMissed()
        {
            Miss();
        }

        private void OnTokenSmashed()
        {
            m_NumOfSmashedTokens++;

            if (m_NumOfSmashedTokens == m_Tokens.Count)
            {
                Smash();
            }
        }

        protected override bool IsDataValid(PlayableDTO playableBase)
        {
            var isDataValid  = base.IsDataValid(playableBase);
            
            if (playableBase.Vertices.Length != m_Tokens.Count)
            {
                Debug.Log($"{nameof(Token)} cannot initialize when number of tokens is not equal to number of vertices.");
                return false; 
            }

            return isDataValid;
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
            base.ResetState();
            m_NumOfSmashedTokens = 0;
        }
    }
}
