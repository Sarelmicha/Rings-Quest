using System;
using System.Net.Http;
using Cysharp.Threading.Tasks;

namespace Happyflow.Core.Network
{
    /// <summary>
    /// An implementation of <see cref="INetworkService"/> using HttpClient.
    /// </summary>
    public class HttpClientService : INetworkService , IDisposable
    {
        private readonly string m_Host;
        private readonly HttpClient m_Client;
        private const string CONTENT_TYPE = "application/json";

        public HttpClientService(string host)
        {
            m_Host = host;
            m_Client = new HttpClient();
        }

        /// <summary>
        /// Sends a GET request to retrieve a resource from the server.
        /// </summary>
        /// <param name="url">The endpoint url of the resource.</param>
        public async UniTask<NetworkResponse> Get(string url)
        {
            var response = await m_Client.GetAsync($"{m_Host}/{url}");
            var responseBody = await response.Content.ReadAsStringAsync();
            return new NetworkResponse(response.IsSuccessStatusCode, responseBody);
        }
        
        /// <summary>
        /// Sends a POST request to create a new resource on the server.
        /// </summary>
        /// <param name="url">The endpoint url of the resource.</param>
        /// <param name="data">The data in JSON format to send in the request body.</param>
        public async UniTask<NetworkResponse> Post(string url, string data)
        {
            var content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(CONTENT_TYPE);
            var response = await m_Client.PostAsync($"{m_Host}/{url}", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            return new NetworkResponse(response.IsSuccessStatusCode, responseBody);
        }

        /// <summary>
        /// Sends a PUT request to update a resource on the server.
        /// </summary>
        /// <param name="url">The endpoint url of the resource.</param>
        /// <param name="data">The data in JSON format to send in the request body.</param>
        public async UniTask<NetworkResponse> Put(string url, string data)
        {
            var content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(CONTENT_TYPE);
            var response = await m_Client.PutAsync($"{m_Host}/{url}", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            return new NetworkResponse(response.IsSuccessStatusCode, responseBody);
        }

        /// <summary>
        /// Sends a DELETE request to delete a resource on the server.
        /// </summary>
        /// <param name="url">The endpoint url of the resource.</param>
        public async UniTask<NetworkResponse> Delete(string url)
        {
            var response = await m_Client.DeleteAsync($"{m_Host}/{url}");
            var responseBody = await response.Content.ReadAsStringAsync();
            return new NetworkResponse(response.IsSuccessStatusCode, responseBody);
        }

        /// <summary>
        /// Dispose the <see cref="HttpClientService"/> components.
        /// </summary>
        public void Dispose()
        {
            m_Client?.Dispose();
        }
    }
}