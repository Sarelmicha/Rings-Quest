namespace Happyflow.Core.Parser
{
    /// <summary>
    /// An interface that provides an API for serialize and deserialize objects.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Serialize an object into a string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>The serialized string.</returns>
        string SerializeObject(object value);

        /// <summary>
        /// Deserialize a string into a concrete object.
        /// </summary>
        /// <param name="value">The string to deserialize.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The deserialized object.</returns>
        T DeserializeObject<T>(string value);
    }
}

