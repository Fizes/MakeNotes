using MakeNotes.Common.Models;

namespace MakeNotes.Notebook.Views.Templates
{
    public partial class PasswordDataGridTemplate : IDynamicElement
    {
        public PasswordDataGridTemplate(PasswordDataGridTemplateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
