namespace MakeNotes.Notebook.Models
{
    /// <summary>
    /// Represents an item inside the password data grid.
    /// </summary>
    public class PasswordDataGridItem
    {
        /// <summary>
        /// Site which the password relates to (can be anything, e.g., website, program, etc.).
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Password to the site.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Optional description.
        /// </summary>
        public string Description { get; set; }
    }
}
