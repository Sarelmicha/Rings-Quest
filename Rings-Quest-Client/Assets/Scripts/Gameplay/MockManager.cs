using Happyflow.Core.Network;
using Happyflow.Core.Parser;
using Happyflow.Core.RetryStrategy;
using Happyflow.Core.ServiceLocator;
using Happyflow.RingsQuest.Gameplay.Level.DTO;
using UnityEngine;

namespace Happyflow.RingsQuest.Gameplay
{
    /// <summary>
    /// Some mock manager for sending a request to the server to gets the level data.
    /// in the future, the request will occured from out of this scope.
    /// </summary>
    public class MockManager : MonoBehaviour
    {
        [SerializeField] private LevelManager m_LevelManager;
        private INetworkService m_NetworkService;
        private IParser m_Parser;
        private RetryStrategyBase m_RetryStrategy;
        
        private void Awake()
        {
            m_Parser = new NewtonsoftJsonParser();
            m_RetryStrategy = new MaxRetryStrategy(3);
        }
        
        private async void Start()
        {
            m_NetworkService = ServiceLocator.Instance.Get<INetworkService>();
            
            var networkResponse = await m_RetryStrategy.TryOperationAsync(() => m_NetworkService.Get("levels/order/0"),
                networkResponse => networkResponse.IsSuccess);

            if (!networkResponse.IsSuccess)
            {
                return;
            }
            
            var levelDTO = m_Parser.DeserializeObject<LevelDTO>(networkResponse.Response);
            var initialize = await m_LevelManager.Initialize(levelDTO.Playables, levelDTO.LevelStatsDTO.Playables.PlayablesCountMap);

            if (!initialize)
            {
                return;
            }

            m_LevelManager.StartLevel();
        }
    }
}
