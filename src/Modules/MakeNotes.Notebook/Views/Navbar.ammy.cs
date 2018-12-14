using MakeNotes.Notebook.ViewModels;

namespace MakeNotes.Notebook.Views
{
    public partial class Navbar
    {
        public Navbar(NavbarViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
