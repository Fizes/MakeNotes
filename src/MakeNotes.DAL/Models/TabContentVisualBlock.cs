namespace MakeNotes.DAL.Models
{
    /// <summary>
    /// Maps a visual block item with the appropriate tab content block.
    /// </summary>
    public class TabContentVisualBlock
    {
        /// <summary>
        /// Unique id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tab content element holding all data rows.
        /// </summary>
        public int TabContentId { get; set; }

        /// <summary>
        /// Single row holding data.
        /// </summary>
        public int VisualBlockId { get; set; }
    }
}
