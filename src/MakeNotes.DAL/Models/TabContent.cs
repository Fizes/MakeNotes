namespace MakeNotes.DAL.Models
{
    /// <summary>
    /// Represents a block of data inside a tab.
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
        /// Type of a visual block containing the data.
        /// </summary>
        public int VisualBlockTypeId { get; set; }
    }
}
