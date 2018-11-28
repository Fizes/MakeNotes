using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace MakeNotes
{
    public class MainWindowViewModel : BindableBase
    {
        private bool _isAddNewTabDialogOpen;
        private string _tabName;

        public MainWindowViewModel()
        {
            AddNewTabCommand = new DelegateCommand(AddNewTab);
            CancelAddNewTabCommand = new DelegateCommand(CancelAddNewTab);
        }

        public bool IsAddNewTabDialogOpen
        {
            get => _isAddNewTabDialogOpen;
            set => SetProperty(ref _isAddNewTabDialogOpen, value);
        }

        public string TabName
        {
            get => _tabName;
            set => SetProperty(ref _tabName, value);
        }

        public ICommand AddNewTabCommand { get; }

        public ICommand CancelAddNewTabCommand { get; }

        private void AddNewTab()
        {
            ResetAddNewTabDialog();
        }

        private void CancelAddNewTab()
        {
            ResetAddNewTabDialog();
        }
        
        private void ResetAddNewTabDialog()
        {
            IsAddNewTabDialogOpen = false;
            TabName = null;
        }
    }
}
