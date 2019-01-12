using System.Collections.Generic;
using MakeNotes.Common.Core;
using MakeNotes.Framework.Events;
using MakeNotes.Framework.Factories;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Core.Commands;
using MakeNotes.Notebook.Core.Notifications;
using MakeNotes.Notebook.Core.Queries;
using MakeNotes.Notebook.Models;
using MakeNotes.Notebook.Views.Templates;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MakeNotes.Notebook.ViewModels
{
    public class TabContentViewModel : BindableBase
    {
        private readonly IMessageBus _messageBus;
        private readonly IEventAggregator _eventAggregator;
        private readonly IViewFactory _viewFactory;
        
        private TabContent _tabContent;

        public TabContentViewModel(IMessageBus messageBus, IEventAggregator eventAggregator, IViewFactory viewFactory)
        {
            _messageBus = messageBus;
            _eventAggregator = eventAggregator;
            _viewFactory = viewFactory;

            _eventAggregator.GetEvent<ApplicationEvent<TabSelected>>().Subscribe(OnTabSelected);

            ActionMenuItems = new[]
            {
                new ActionMenuItem
                {
                    Tooltip = "Add a new password sheet",
                    Icon = "TablePlus",
                    Action = new DelegateCommand(AddNewGrid)
                }
            };
        }

        public IEnumerable<ActionMenuItem> ActionMenuItems { get; }

        public int CurrentTabId { get; private set; }

        public TabContent TabContent
        {
            get => _tabContent;
            set => SetProperty(ref _tabContent, value);
        }

        private async void OnTabSelected(TabSelected notification)
        {
            CurrentTabId = notification.Id;

            var tabContent = await _messageBus.SendAsync(new GetTabContentByTabId(notification.Id));
        }

        private async void AddNewGrid()
        {
            var lastTabContentOrder = await _messageBus.SendAsync(new GetLastTabContentOrder(CurrentTabId));
            var id = await _messageBus.SendAsync(new AddTabContent(CurrentTabId, lastTabContentOrder + 1, VisualBlockTypes.PasswordSheet));

            TabContent = new TabContent { Id = id, TabId = CurrentTabId };

            var grid = _viewFactory.Create<PasswordDataGridTemplate>();
            TabContent.Items.Add(grid);
        }
    }
}
