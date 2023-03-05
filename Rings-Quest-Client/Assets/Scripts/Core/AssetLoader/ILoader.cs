namespace Happyflow.Core
{
    /// <summary>
    /// An interface for a class that loads objects.
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Gets the count of loaded objects.
        /// </summary>
        int LoadedObjectsCount { get; }
    }
}