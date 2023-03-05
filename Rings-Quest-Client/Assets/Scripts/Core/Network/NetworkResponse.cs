namespace Happyflow.Core.Network
{
    /// <summary>
    /// Holds all the data that received from the server response.
    /// </summary>
    public readonly struct NetworkResponse
    {
        /// <summary>
        /// A flag that indicate whether the response is success or not (200 OK)
        /// </summary>
        public bool IsSuccess { get; }
        
        /// <summary>
        /// The response from the server.
        /// </summary>
        public string Response { get; }
        
        public NetworkResponse(bool isSuccess, string response)
        {
            IsSuccess = isSuccess;
            Response = response;
        }
    }
}

