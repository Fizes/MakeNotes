﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MakeNotes.Common.Core;
using MakeNotes.Common.Infrastructure.Extensions;
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
        private NavbarTabItem _selectedTab;
        private NavbarTabItemObservableCollection _tabs;

        public NavbarViewModel(IEventAggregator eventAggregator, IMessageBus messageBus, IQueryDispatcher queryDispatcher)
        {
            _eventAggregator = eventAggregator;
            _messageBus = messageBus;
            _queryDispatcher = queryDispatcher;

            LoadTabsCommand = new DelegateCommand(LoadTabs);
            AddTabCommand = new DelegateCommand(AddTab);
            DeleteTabCommand = new DelegateCommand<NavbarTabItem>(DeleteTab);
        }

        #region Properties
        public string TabName
        {
            get => _tabName;
            set => SetProperty(ref _tabName, value);
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
                    _eventAggregator.GetEvent<TabSelectedEvent>().Publish(_selectedTab.Id.GetValueOrDefault());
                }
            }
        }

        public NavbarTabItemObservableCollection Tabs
        {
            get => _tabs;
            private set => SetProperty(ref _tabs, value);
        }

        public ICommand LoadTabsCommand { get; }

        public ICommand AddTabCommand { get; }

        public ICommand DeleteTabCommand { get; }
        #endregion

        private async void LoadTabs()
        {
            var tabs = await _queryDispatcher.ExecuteAsync(new GetAllTabs());
            var items = tabs.Select(t => new NavbarTabItem(t.Name, t.Order) { Id = t.Id });
            Tabs = new NavbarTabItemObservableCollection(items);
        }

        private async void AddTab()
        {
            await DialogManager.Show<AddTabDialog>(viewModel: this, OnCloseAddTabDialog);
        }

        private async void DeleteTab(NavbarTabItem tabItem)
        {
            await DialogManager.Show<DeleteTabDialog>(viewModel: null, async result => await OnCloseDeleteTabDialog(result, tabItem));
        }

        private async Task OnCloseDeleteTabDialog(DialogResult result, NavbarTabItem tabItem)
        {
            if (result != DialogResult.Accepted || !tabItem.Id.HasValue)
            {
                return;
            }
            
            await _messageBus.SendAsync(new DeleteTab(tabItem.Id.Value));

            if (SelectedTab == tabItem)
            {
                SelectedTab = Tabs.GetPreviousElement(tabItem);
            }

            Tabs.Remove(tabItem);
        }

        private async void OnCloseAddTabDialog(DialogResult result)
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
                SelectedTab = newItem;
            }

            TabName = null;
        }

        private async Task PrependInitialTab(int initialTabOrder)
        {
            if (initialTabOrder != 0)
            {
                return;
            }
            
            var tab = Tabs[initialTabOrder];
            if (!tab.Id.HasValue)
            {
                await _messageBus.SendAsync(new CreateTab(tab.Header, tab.Order));
            }
        }
    }
}
