using System.Linq;
using System.Windows.Input;
using MakeNotes.Common.Models;
using MakeNotes.Notebook.Collections;
using Prism.Commands;
using Prism.Mvvm;

namespace MakeNotes
{
    public class MainWindowViewModel : BindableBase
    {
        private bool _isAddingTabStarted;
        private string _tabName;
        private int _selectedTabIndex;

        public MainWindowViewModel()
        {
            AddTabCommand = new DelegateCommand(AddTab).ObservesCanExecute(() => IsAddingTabStarted);
            CancelAddTabCommand = new DelegateCommand(CancelAddTab).ObservesCanExecute(() => IsAddingTabStarted);
        }

        public bool IsAddingTabStarted
        {
            get => _isAddingTabStarted;
            set => SetProperty(ref _isAddingTabStarted, value);
        }

        public string TabName
        {
            get => _tabName;
            set => SetProperty(ref _tabName, value);
        }

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        public NavbarTabItemObservableCollection Tabs { get; } = new NavbarTabItemObservableCollection();

        public ICommand AddTabCommand { get; }

        public ICommand CancelAddTabCommand { get; }

        private void AddTab()
        {
            var maxItemOrder = Tabs.Max(t => t.Order);
            var newItem = new NavbarTabItem(TabName, maxItemOrder + 1);

            Tabs.Add(newItem);
            SelectedTabIndex = newItem.Order;

            ResetDialogStateToDefault();
        }

        private void CancelAddTab()
        {
            ResetDialogStateToDefault();
        }
        
        private void ResetDialogStateToDefault()
        {
            IsAddingTabStarted = false;
            TabName = null;
        }
    }
}
