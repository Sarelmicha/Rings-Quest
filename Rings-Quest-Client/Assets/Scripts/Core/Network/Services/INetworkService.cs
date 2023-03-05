using Cysharp.Threading.Tasks;
using Happyflow.Core.ServiceLocator;

namespace Happyflow.Core.Network
{
    /// <summary>
    /// An interface that provides an API for sending CRUD requests to a server.
    /// </summary>
    public interface INetworkService : IGameService
    {
        /// <summary>
        /// Sends a GET request to retrieve a resource from the server.
        /// </summary>
        /// <param name="url">The endpoint url of the resource.</param>
        UniTask<NetworkResponse> Get(string url);
        
        /// <summary>
        /// Sends a POST request to create a new resource on the server.
        /// </summary>
        /// <param name="url">The endpoint url of the resource.</param>
        /// <param name="data">The data in JSON format to send in the request body.</param>
        UniTask<NetworkResponse> Post(string url, string data);

        /// <summary>
        /// Sends a PUT request to update a resource on the server.
        /// </summary>
        /// <param name="url">The endpoint url of the resource.</param>
        /// <param name="data">The data in JSON format to send in the request body.</param>
        UniTask<NetworkResponse> Put(string url, string data);

        /// <summary>
        /// Sends a DELETE request to delete a resource on the server.
        /// </summary>
        /// <param name="url">The endpoint url of the resource.</param>
        UniTask<NetworkResponse> Delete(string url);
    }
}

