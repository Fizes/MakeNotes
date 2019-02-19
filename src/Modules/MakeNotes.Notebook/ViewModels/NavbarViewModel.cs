using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MakeNotes.Common.Core;
using MakeNotes.Common.Infrastructure.Extensions;
using MakeNotes.Common.Models;
using MakeNotes.Framework.Models;
using MakeNotes.Framework.Services;
using MakeNotes.Notebook.Collections;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Core.Commands;
using MakeNotes.Notebook.Core.Notifications;
using MakeNotes.Notebook.Core.Queries;
using MakeNotes.Notebook.Properties;
using MakeNotes.Notebook.Templates.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace MakeNotes.Notebook.ViewModels
{
    public class NavbarViewModel : BindableBase
    {
        private readonly IMessageBus _messageBus;
        private readonly IInteractionService _interactionService;

        private string _tabName;
        private NavbarTabItem _selectedTab;
        private NavbarTabItemObservableCollection _tabs = new NavbarTabItemObservableCollection();

        public NavbarViewModel(IMessageBus messageBus, IInteractionService interactionService)
        {
            _messageBus = messageBus;
            _interactionService = interactionService;

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
                    _messageBus.Publish(new TabSelected(_selectedTab.Id.GetValueOrDefault()));
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
            var tabs = await _messageBus.SendAsync(new GetAllTabs());
            var items = tabs.Select(t => new NavbarTabItem(t.Name, t.Order) { Id = t.Id });
            Tabs = new NavbarTabItemObservableCollection(items);
        }

        private async void AddTab()
        {
            await _interactionService.Show<AddTabDialog>(viewModel: this, OnCloseAddTabDialog);
        }

        private async void DeleteTab(NavbarTabItem tabItem)
        {
            var canBeDeletedWithoutConfirmation = await CanTabBeDeletedWithoutConfirmation(tabItem);
            if (canBeDeletedWithoutConfirmation)
            {
                await OnCloseDeleteTabDialog(DialogResult.Accepted, tabItem);
            }
            else
            {
                await _interactionService.ShowConfirmation(
                    Resources.DeleteTabTitle,
                    Resources.DeleteTabText,
                    async result => await OnCloseDeleteTabDialog(result, tabItem));
            }
        }

        private async Task<bool> CanTabBeDeletedWithoutConfirmation(NavbarTabItem tabItem)
        {
            if (!tabItem.Id.HasValue)
            {
                return true;
            }

            var tabContentCount = await _messageBus.SendAsync(new GetCountOfTabContentByTabId(tabItem.Id.Value));

            return tabContentCount == 0;
        }

        private async Task OnCloseDeleteTabDialog(DialogResult result, NavbarTabItem tabItem)
        {
            if (result != DialogResult.Accepted)
            {
                return;
            }

            if (tabItem.Id.HasValue)
            {
                await _messageBus.SendAsync(new DeleteTab(tabItem.Id.Value));
            }

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
                var lastTabOrder = await _messageBus.SendAsync(new GetLastTabOrder());

                var tabOrder = lastTabOrder + 1;
                var tabName = String.IsNullOrWhiteSpace(TabName) ? DefaultValues.DefaultTabName : TabName;
                var newItem = new NavbarTabItem(tabName, tabOrder);

                await PrependInitialTab();
                await CreateTab(newItem);

                Tabs.Add(newItem);
                SelectedTab = newItem;
            }

            TabName = null;
        }

        private async Task PrependInitialTab()
        {
            var tab = Tabs.First();
            if (!tab.Id.HasValue)
            {
                await CreateTab(tab);
            }
        }

        private async Task CreateTab(NavbarTabItem tabItem)
        {
            var id = await _messageBus.SendAsync(new CreateTab(tabItem.Header, tabItem.Order));
            tabItem.Id = id;
        }
    }
}
