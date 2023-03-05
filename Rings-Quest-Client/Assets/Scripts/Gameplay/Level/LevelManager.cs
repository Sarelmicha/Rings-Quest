using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Happyflow.Core.EventAggregator;
using Happyflow.RingsQuest.Gameplay.Level.DTO;
using Happyflow.RingsQuest.Gameplay.Playable.DTO;
using Happyflow.RingsQuest.Gameplay.Ring;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay
{
    /// <summary>
    /// The LevelManager manages the level and all of its components.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private RingManager m_RingManager;
        private IEventAggregator m_LevelEventAggregator;

        private void Awake()
        {
            m_LevelEventAggregator = new EventAggregator();
        }

        public async UniTask<bool> Initialize(List<PlayableDTO> playables, Dictionary<string, int> playableCountMap)
        {
            return await m_RingManager.Initialize(playables, playableCountMap, m_LevelEventAggregator);
        }

        public void StartLevel()
        {
            m_RingManager.Spawn();
        }
    }
}

