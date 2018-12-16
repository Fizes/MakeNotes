using MakeNotes.Notebook.ViewModels;

namespace MakeNotes.Notebook.Views
{
    public partial class TabContent
    {
        public TabContent(TabContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
