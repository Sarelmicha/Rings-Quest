using Newtonsoft.Json;

namespace Happyflow.Core.Parser
{
    public class NewtonsoftJsonParser : IParser
    {
        /// <summary>
        /// Serialize an object into a string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>The serialized string.</returns>
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Deserialize a string into a concrete object.
        /// </summary>
        /// <param name="value">The string to deserialize.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The deserialized object.</returns>
        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }

}
