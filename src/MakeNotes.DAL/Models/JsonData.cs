namespace MakeNotes.DAL.Models
{
    /// <summary>
    /// Serves to store an object as json string which can be then deserialized using the type name.
    /// </summary>
    internal sealed class JsonData
    {
        /// <summary>
        /// Clr type full name.
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Stored object.
        /// </summary>
        public object Data { get; set; }
    }
}
