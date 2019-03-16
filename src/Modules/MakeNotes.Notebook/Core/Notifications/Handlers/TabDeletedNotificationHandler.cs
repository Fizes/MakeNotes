using System.Collections.Generic;
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

        private Task<IEnumerable<string>> GetTabContentVisualBlockSysNamesAsync(int tabId)
        {
            var query = new QueryObject(
                @"SELECT [SysName]
                  FROM [VisualBlockType]
                  WHERE [Id] = (SELECT DISTINCT [VisualBlockTypeId]
                                FROM [TabContent]
                                WHERE [TabId] = @TabId)", new { TabId = tabId });

            return _repository.QueryAsync<string>(query);
        }

        private Task DeleteVisualBlocksAsync(string tableName, int tabId)
        {
            var query = new QueryObject(
                    $@"DELETE FROM [{tableName}]
                       WHERE [Id] IN (SELECT [VisualBlockId]
                                      FROM [TabContentVisualBlock]
                                      WHERE [TabContentId] = (SELECT [Id] FROM [TabContent] WHERE [TabId] = @TabId))",
                new { TabId = tabId });

            return _repository.ExecuteAsync(query);
        }

        private Task DeleteTabContentVisualBlocksAsync(int tabId)
        {
            var query = new QueryObject(
                @"DELETE FROM [TabContentVisualBlock]
                  WHERE [TabContentId] IN (SELECT [Id]
                                           FROM [TabContent]
                                           WHERE [TabId] = @TabId)",
                new { TabId = tabId });

            return _repository.ExecuteAsync(query);
        }

        private Task DeleteTabContentsAsync(int tabId)
        {
            var query = new QueryObject(
                @"DELETE FROM [TabContent]
                  WHERE [TabId] = @TabId", new { TabId = tabId });

            return _repository.ExecuteAsync(query);
        }

        public async void Handle(TabDeleted notification)
        {
            _eventAggregator.GetEvent<ApplicationEvent<TabDeleted>>().Publish(notification);

            var visualBlockSysNames = await GetTabContentVisualBlockSysNamesAsync(notification.Id);
            foreach (var sysName in visualBlockSysNames)
            {
                await DeleteVisualBlocksAsync(sysName, notification.Id);
            }

            await DeleteTabContentVisualBlocksAsync(notification.Id);

            await DeleteTabContentsAsync(notification.Id);
        }
    }
}
