using System.Threading.Tasks;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.DAL.Core;
using MakeNotes.Framework.Events;
using Prism.Events;

namespace MakeNotes.Notebook.Core.Notifications.Handlers
{
    public class TabDeletedNotificationHandler : INotificationHandler<TabDeleted>
    {
        private readonly IRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        public TabDeletedNotificationHandler(IRepository repository, IEventAggregator eventAggregator)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
        }

        private Task<string> GetTabContentVisualBlockSysName(int tabId)
        {
            var query = new QueryObject(
                @"SELECT [SysName]
                  FROM [VisualBlockType]
                  WHERE [Id] = (SELECT DISTINCT [VisualBlockTypeId]
                                FROM [TabContent]
                                WHERE [TabId] = @TabId)", new { TabId = tabId });

            return _repository.QueryFirstOrDefaultAsync<string>(query);
        }

        public async void Handle(TabDeleted notification)
        {
            _eventAggregator.GetEvent<ApplicationEvent<TabDeleted>>().Publish(notification);

            var visualBlockSysName = await GetTabContentVisualBlockSysName(notification.Id);
            if (visualBlockSysName == null)
            {
                return;
            }

            var query = new QueryObject(
                $@"DELETE
                   FROM [TabContent]
                   WHERE [TabId] = @TabId;", new { TabId = notification.Id });

            await _repository.ExecuteAsync(query);
        }
    }
}
