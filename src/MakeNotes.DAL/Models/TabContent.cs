namespace MakeNotes.DAL.Models
{
    /// <summary>
    /// Represents an element associated with a tab which together forms its content.
    /// </summary>
    public class TabContent
    {
        /// <summary>
        /// Unique id associated with a particular tab content.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of a tab it belongs to.
        /// </summary>
        public int TabId { get; set; }

        /// <summary>
        /// Zero-based index of the element.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Data associated with a particular element as a json string.
        /// </summary>
        public string Data { get; set; }
    }
}
