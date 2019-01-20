using MakeNotes.Notebook.Templates.VisualBlocks.ViewModels;

namespace MakeNotes.Notebook.Models
{
    /// <summary>
    /// Metadata that is used to resolve a visual block template.
    /// </summary>
    public class VisualBlockTemplate
    {
        /// <summary>
        /// Associated tab content id.
        /// </summary>
        public int TabContentId { get; set; }

        /// <summary>
        /// Template name.
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// View model.
        /// </summary>
        public IVisualBlockViewModel DataContext { get; set; }
    }
}
