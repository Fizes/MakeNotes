namespace MakeNotes.DAL.Models
{
    public class Tab
    {
        /// <summary>
        /// Unique id associated with a particular tab.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tab title.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Zero-based index of the tab inside the navbar.
        /// </summary>
        public int Order { get; set; }
    }
}
