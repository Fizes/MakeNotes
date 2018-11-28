namespace MakeNotes.Common.Models
{
    /// <summary>
    /// Represents a tab item inside the navbar.
    /// </summary>
    public class NavbarTabItem
    {
        /// <summary>
        /// Tab title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Zero-based index of the tab inside the navbar.
        /// </summary>
        public int Order { get; set; }
    }
}
