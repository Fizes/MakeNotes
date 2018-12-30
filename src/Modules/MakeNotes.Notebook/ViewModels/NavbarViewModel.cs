using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MakeNotes.Common.Core;
using MakeNotes.Common.Models;
using MakeNotes.Framework.Controls;
using MakeNotes.Framework.Models;
using MakeNotes.Notebook.Collections;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Core.Commands;
using MakeNotes.Notebook.Core.Queries;
using MakeNotes.Notebook.Events;
using MakeNotes.Notebook.Views.Templates;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MakeNotes.Notebook.ViewModels
{
    public class NavbarViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageBus _messageBus;
        private readonly IQueryDispatcher _queryDispatcher;

        private string _tabName;
        private int _selectedTabIndex;
        private NavbarTabItem _selectedTab;
        private NavbarTabItemObservableCollection _tabs = new NavbarTabItemObservableCollection();

        public NavbarViewModel(IEventAggregator eventAggregator, IMessageBus messageBus, IQueryDispatcher queryDispatcher)
        {
            _eventAggregator = eventAggregator;
            _messageBus = messageBus;
            _queryDispatcher = queryDispatcher;

            LoadTabsCommand = new DelegateCommand(LoadTabs);
            AddTabCommand = new DelegateCommand(AddTab);
        }

        #region Properties
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

                if (_selectedTab != null && _selectedTab != oldValue)
                {
                    _eventAggregator.GetEvent<TabChangedEvent>().Publish(_selectedTab.Id);
                }
            }
        }

        public NavbarTabItemObservableCollection Tabs
        {
            get => _tabs;
            set => SetProperty(ref _tabs, value);
        }

        public ICommand LoadTabsCommand { get; }

        public ICommand AddTabCommand { get; }
        #endregion

        private async void LoadTabs()
        {
            var tabs = await _queryDispatcher.ExecuteAsync(new GetAllTabs());
            var items = tabs.Select(t => new NavbarTabItem(t.Name, t.Order));
            Tabs = new NavbarTabItemObservableCollection(items);
        }

        private async void AddTab()
        {
            await DialogManager.Show<AddTabDialog>(viewModel: this, OnCloseDialog);
        }

        private async void OnCloseDialog(DialogResult result)
        {
            if (result == DialogResult.Accepted)
            {
                var lastTabOrder = await _queryDispatcher.ExecuteAsync(new GetLastTabOrder());

                var tabOrder = lastTabOrder + 1;
                var tabName = String.IsNullOrWhiteSpace(TabName) ? DefaultValues.DefaultTabName : TabName;
                var newItem = new NavbarTabItem(tabName, tabOrder);

                await PrependInitialTab(lastTabOrder);
                await _messageBus.SendAsync(new CreateTab(newItem.Header, newItem.Order));

                Tabs.Add(newItem);
                SelectedTabIndex = newItem.Order;
            }

            TabName = null;
        }

        private async Task PrependInitialTab(int initialTabOrder)
        {
            if (initialTabOrder == 0)
            {
                var tab = Tabs[initialTabOrder];
                await _messageBus.SendAsync(new CreateTab(tab.Header, tab.Order));
            }
        }
    }
}
