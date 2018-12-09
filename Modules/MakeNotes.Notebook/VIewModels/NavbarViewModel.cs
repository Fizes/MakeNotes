using System.Linq;
using System.Windows.Input;
using MakeNotes.Common.Models;
using MakeNotes.Framework.Controls;
using MakeNotes.Notebook.Collections;
using MakeNotes.Notebook.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace MakeNotes.Notebook.ViewModels
{
    public class NavbarViewModel : BindableBase
    {
        private string _tabName;
        private int _selectedTabIndex;

        public NavbarViewModel()
        {
            AddTabCommand = new DelegateCommand(AddTab);
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

        private async void AddTab()
        {
            await DialogManager.Show<AddTabDialog>(viewModel: this, OnAddTab, result => ResetDialogState());
        }

        private void OnAddTab()
        {
            var maxItemOrder = Tabs.Max(t => t.Order);
            var newItem = new NavbarTabItem(TabName, maxItemOrder + 1);

            Tabs.Add(newItem);
            SelectedTabIndex = newItem.Order;
        }

        private void ResetDialogState()
        {
            TabName = null;
        }
    }
}
