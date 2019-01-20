using System.Windows.Input;

namespace MakeNotes.Notebook.Templates.VisualBlocks.ViewModels
{
    public interface IVisualBlockViewModel
    {
        ICommand InitializeCommand { get; }
    }
}
