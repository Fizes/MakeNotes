using System.Windows.Input;

namespace MakeNotes.Notebook.Models
{
    /// <summary>
    /// Represents an item inside the action menu.
    /// </summary>
    public class ActionMenuItem
    {
        /// <summary>
        /// Tooltip shown on the item.
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Item icon kind.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Action associated with the item.
        /// </summary>
        public ICommand Action { get; set; }

        /// <summary>
        /// Parameter passed to the action.
        /// </summary>
        public object ActionParameter { get; set; }
    }
}
