using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly VisualBlockTemplateFactory _visualBlockTemplateFactory;

        private readonly Dictionary<int, string> _cachedVisualBlockTypes = new Dictionary<int, string>();
        private readonly Dictionary<int, ObservableCollection<VisualBlockTemplate>> _cachedTabContent =
            new Dictionary<int, ObservableCollection<VisualBlockTemplate>>();

        private ObservableCollection<VisualBlockTemplate> _content;

        public TabContentViewModel(IMessageBus messageBus, IEventAggregator eventAggregator, VisualBlockTemplateFactory visualBlockTemplateFactory)
        {
            _messageBus = messageBus;
            _visualBlockTemplateFactory = visualBlockTemplateFactory;

            eventAggregator.GetEvent<ApplicationEvent<TabSelected>>().Subscribe(OnTabSelected, ThreadOption.UIThread);
            eventAggregator.GetEvent<ApplicationEvent<TabDeleted>>().Subscribe(OnTabDeleted, ThreadOption.BackgroundThread);
            eventAggregator.GetEvent<ApplicationEvent<TabContentDeleted>>().Subscribe(OnTabContentDeleted, ThreadOption.UIThread);

            var addVisualBlockCommand = new DelegateCommand<string>(AddVisualBlock);

            ActionMenuItems = new[]
            {
                new ActionMenuItem
                {
                    Tooltip = "Add a new password sheet",
                    Icon = "TablePlus",
                    Action = addVisualBlockCommand,
                    ActionParameter = VisualBlockTypes.PasswordSheet
                }
            };
        }

        public IEnumerable<ActionMenuItem> ActionMenuItems { get; }

        public ObservableCollection<VisualBlockTemplate> Content
        {
            get => _content;
            private set => SetProperty(ref _content, value);
        }

        public int CurrentTabId { get; private set; }

        private async Task<string> GetVisualBlockTypeSysNameAsync(int visualBlockTypeId)
        {
            if (!_cachedVisualBlockTypes.ContainsKey(visualBlockTypeId))
            {
                var type = await _messageBus.SendAsync(new FindVisualBlockTypeById(visualBlockTypeId));
                _cachedVisualBlockTypes.Add(visualBlockTypeId, type.SysName);

                return type.SysName;
            }

            return _cachedVisualBlockTypes[visualBlockTypeId];
        }

        private async Task<ObservableCollection<VisualBlockTemplate>> GetVisualBlocksAsync(int tabId)
        {
            var templates = new ObservableCollection<VisualBlockTemplate>();
            var tabContent = await _messageBus.SendAsync(new GetTabContentByTabId(tabId));

            foreach (var content in tabContent)
            {
                var typeName = await GetVisualBlockTypeSysNameAsync(content.VisualBlockTypeId);
                templates.Add(_visualBlockTemplateFactory.Create(content.Id, typeName));
            }

            return templates;
        }

        private async void OnTabSelected(TabSelected notification)
        {
            CurrentTabId = notification.Id;

            if (!_cachedTabContent.ContainsKey(CurrentTabId))
            {
                var tabContent = await GetVisualBlocksAsync(CurrentTabId);
                _cachedTabContent.Add(CurrentTabId, tabContent);
            }

            Content = _cachedTabContent[CurrentTabId];
        }

        private void OnTabDeleted(TabDeleted notification)
        {
            _cachedTabContent.Remove(notification.Id);
        }

        private void OnTabContentDeleted(TabContentDeleted notification)
        {
            var deletedItem = Content.Single(t => t.TabContentId == notification.Id);
            Content.Remove(deletedItem);
        }

        private async void AddVisualBlock(string templateName)
        {
            var lastTabContentOrder = await _messageBus.SendAsync(new GetLastTabContentOrder(CurrentTabId));
            var id = await _messageBus.SendAsync(new AddTabContent(CurrentTabId, lastTabContentOrder + 1, templateName));

            Content.Add(_visualBlockTemplateFactory.Create(id, templateName));
        }
    }
}
