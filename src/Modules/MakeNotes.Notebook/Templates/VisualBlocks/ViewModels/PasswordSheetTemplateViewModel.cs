using System.Windows.Input;
using MakeNotes.DAL.Models;
using Prism.Commands;

namespace MakeNotes.Notebook.Templates.VisualBlocks.ViewModels
{
    public class PasswordSheetTemplateViewModel : VisualBlockViewModelBase<PasswordSheet>
    {
        private string _title = "Passwords";

        public PasswordSheetTemplateViewModel()
        {
            AddItemCommand = new DelegateCommand(AddItem);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand AddItemCommand { get; }

        protected override void Initialize(int tabContentId)
        {
        }

        private void AddItem()
        {
            Items.Add(new PasswordSheet());
        }
    }
}
