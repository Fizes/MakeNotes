using System.Threading.Tasks;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.DAL.Core;
using MakeNotes.Framework.Events;
using Prism.Events;

namespace MakeNotes.Notebook.Core.Notifications.Handlers
{
    public class TabContentDeletedNotificationHandler : INotificationHandler<TabContentDeleted>
    {
        private readonly IRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        public TabContentDeletedNotificationHandler(IRepository repository, IEventAggregator eventAggregator)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
        }

        private Task<string> GetTabContentVisualBlockSysName(int tabContentId)
        {
            var query = new QueryObject(
                @"SELECT [SysName]
                  FROM [VisualBlockType]
                  WHERE [Id] = (SELECT DISTINCT [VisualBlockTypeId]
                                FROM [TabContent]
                                WHERE [Id] = @TabContentId)", new { TabContentId = tabContentId });
            
            return _repository.QuerySingleAsync<string>(query);
        }

        public async void Handle(TabContentDeleted notification)
        {
            _eventAggregator.GetEvent<ApplicationEvent<TabContentDeleted>>().Publish(notification);

            var visualBlockSysName = await GetTabContentVisualBlockSysName(notification.Id);
            
            var query = new QueryObject(
                $@"DELETE FROM [{visualBlockSysName}]
                   WHERE [Id] IN (SELECT [VisualBlockId]
                                  FROM [TabContentVisualBlock]
                                  WHERE [TabContentId] = @Id);", notification);

            await _repository.ExecuteAsync(query);
            
            query = new QueryObject(
                @"DELETE FROM [TabContentVisualBlock]
                  WHERE [TabContentId] = @Id;", notification);

            await _repository.ExecuteAsync(query);

            query = new QueryObject(
                @"DELETE FROM [TabContent]
                  WHERE [Id] = @Id;", notification);

            await _repository.ExecuteAsync(query);
        }
    }
}
