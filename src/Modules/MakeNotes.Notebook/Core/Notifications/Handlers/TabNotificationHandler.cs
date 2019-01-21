using System.Threading.Tasks;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.DAL.Core;
using MakeNotes.Framework.Events;
using Prism.Events;

namespace MakeNotes.Notebook.Core.Notifications.Handlers
{
    public class TabNotificationHandler : INotificationHandler<TabDeleted>,
                                          INotificationHandler<TabSelected>
    {
        private readonly IRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        public TabNotificationHandler(IRepository repository, IEventAggregator eventAggregator)
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

        public void Handle(TabSelected notification)
        {
            _eventAggregator.GetEvent<ApplicationEvent<TabSelected>>().Publish(notification);
        }
    }
}
