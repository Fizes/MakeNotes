namespace MakeNotes.Notebook.Views.Templates
{
    public partial class PasswordDataGridTemplate
    {
        public PasswordDataGridTemplate(PasswordDataGridTemplateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
