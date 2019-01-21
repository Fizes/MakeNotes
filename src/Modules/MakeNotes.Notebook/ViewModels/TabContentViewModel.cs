using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MakeNotes.Common.Core;
using MakeNotes.Framework.Events;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Core.Commands;
using MakeNotes.Notebook.Core.Notifications;
using MakeNotes.Notebook.Core.Queries;
using MakeNotes.Notebook.Models;
using MakeNotes.Notebook.Templates;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MakeNotes.Notebook.ViewModels
{
    public class TabContentViewModel : BindableBase
    {
        private readonly IMessageBus _messageBus;
        private readonly IEventAggregator _eventAggregator;
        private readonly VisualBlockTemplateFactory _visualBlockTemplateFactory;

        public TabContentViewModel(IMessageBus messageBus, IEventAggregator eventAggregator, VisualBlockTemplateFactory visualBlockTemplateFactory)
        {
            _messageBus = messageBus;
            _eventAggregator = eventAggregator;
            _visualBlockTemplateFactory = visualBlockTemplateFactory;

            _eventAggregator.GetEvent<ApplicationEvent<TabSelected>>().Subscribe(OnTabSelected, ThreadOption.UIThread);

            AddVisualBlockCommand = new DelegateCommand<string>(AddVisualBlock);

            ActionMenuItems = new[]
            {
                new ActionMenuItem
                {
                    Tooltip = "Add a new password sheet",
                    Icon = "TablePlus",
                    Action = AddVisualBlockCommand,
                    ActionParameter = VisualBlockTypes.PasswordSheet
                }
            };
        }

        public IEnumerable<ActionMenuItem> ActionMenuItems { get; }

        public ObservableCollection<VisualBlockTemplate> Content { get; } = new ObservableCollection<VisualBlockTemplate>();

        public int CurrentTabId { get; private set; }

        public ICommand AddVisualBlockCommand { get; }

        private async void OnTabSelected(TabSelected notification)
        {
            CurrentTabId = notification.Id;
            Content.Clear();

            var tabContent = await _messageBus.SendAsync(new GetTabContentByTabId(notification.Id));
            foreach (var content in tabContent)
            {
                var type = await _messageBus.SendAsync(new FindVisualBlockTypeById(content.VisualBlockTypeId));
                Content.Add(_visualBlockTemplateFactory.Create(content.Id, type.SysName));
            }
        }

        private async void AddVisualBlock(string templateName)
        {
            var lastTabContentOrder = await _messageBus.SendAsync(new GetLastTabContentOrder(CurrentTabId));
            var id = await _messageBus.SendAsync(new AddTabContent(CurrentTabId, lastTabContentOrder + 1, templateName));

            Content.Add(_visualBlockTemplateFactory.Create(id, templateName));
        }
    }
}
