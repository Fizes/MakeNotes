using MakeNotes.Notebook.ViewModels;

namespace MakeNotes.Notebook.Views
{
    public partial class TabContentView
    {
        public TabContentView(TabContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
