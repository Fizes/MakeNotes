using System;
using System.Linq;
using System.Windows.Input;
using MakeNotes.Common.Models;
using MakeNotes.Framework.Controls;
using MakeNotes.Framework.Models;
using MakeNotes.Notebook.Collections;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Events;
using MakeNotes.Notebook.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MakeNotes.Notebook.ViewModels
{
    public class NavbarViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        private string _tabName;
        private int _selectedTabIndex;
        private NavbarTabItem _selectedTab;

        public NavbarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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

        public NavbarTabItem SelectedTab
        {
            get => _selectedTab;
            set
            {
                var oldValue = _selectedTab;
                SetProperty(ref _selectedTab, value);

                if (_selectedTab != oldValue)
                {
                    _eventAggregator.GetEvent<TabChangedEvent>().Publish(_selectedTab.Id);
                }
            }
        }
        
        public NavbarTabItemObservableCollection Tabs { get; } = new NavbarTabItemObservableCollection();
        
        public ICommand AddTabCommand { get; }

        private async void AddTab()
        {
            await DialogManager.Show<AddTabDialog>(viewModel: this, OnCloseDialog);
        }
        
        private void OnCloseDialog(DialogResult result)
        {
            if (result == DialogResult.Accepted)
            {
                var tabName = String.IsNullOrWhiteSpace(TabName) ? DefaultValues.DefaultTabName : TabName;
                var tabOrder = Tabs.Max(t => t.Order) + 1;
                var newItem = new NavbarTabItem(tabName, tabOrder);

                Tabs.Add(newItem);
                SelectedTabIndex = newItem.Order;
            }

            TabName = null;
        }
    }
}
