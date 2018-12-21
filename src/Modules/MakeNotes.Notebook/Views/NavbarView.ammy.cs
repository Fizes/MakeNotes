using MakeNotes.Notebook.ViewModels;

namespace MakeNotes.Notebook.Views
{
    public partial class NavbarView
    {
        public NavbarView(NavbarViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
