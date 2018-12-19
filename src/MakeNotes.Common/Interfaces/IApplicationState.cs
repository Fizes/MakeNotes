namespace MakeNotes.Common.Interfaces
{
    /// <summary>
    /// Represents a storage of global application variables.
    /// </summary>
    public interface IApplicationState
    {
        /// <summary>
        /// Stores the value using the specified key.
        /// </summary>
        /// <param name="key">Key by which the value can be accessed.</param>
        /// <param name="value">Stored value.</param>
        void SetValue(string key, object value);

        /// <summary>
        /// Retrieves a value by the specified key.
        /// </summary>
        /// <typeparam name="T">Variable type.</typeparam>
        /// <param name="key">Key by which the value can be accessed.</param>
        /// <returns></returns>
        T GetValue<T>(string key);
    }
}
