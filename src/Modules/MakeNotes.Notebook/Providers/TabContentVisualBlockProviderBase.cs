using System.Collections.Generic;
using System.Threading.Tasks;
using MakeNotes.Common.Models;
using MakeNotes.DAL.Core;

namespace MakeNotes.Notebook.Providers
{
    public abstract class TabContentVisualBlockProviderBase<TVisualBlock> where TVisualBlock : IVisualBlock
    {
        protected TabContentVisualBlockProviderBase(IRepository repository)
        {
            Repository = repository;
        }

        protected IRepository Repository { get; }

        public abstract string SysName { get; }

        protected abstract Task<int> CreateVisualBlockAsync(TVisualBlock visualBlock);

        public abstract Task UpdateVisualBlockAsync(TVisualBlock visualBlock);

        public Task<IEnumerable<TVisualBlock>> GetVisualBlocksAsync(int tabContentId)
        {
            var query = new QueryObject(
                $@"SELECT vb.*
                   FROM [{SysName}] vb
                   INNER JOIN [TabContentVisualBlock] tvb ON tvb.[VisualBlockId] = vb.[Id]
                   WHERE tvb.[TabContentId] = @TabContentId", new { TabContentId = tabContentId });

            return Repository.QueryAsync<TVisualBlock>(query);
        }

        public async Task<int> CreateVisualBlockAsync(int tabContentId, TVisualBlock visualBlock)
        {
            var visualBlockId = await CreateVisualBlockAsync(visualBlock);

            var query = new QueryObject(
                $@"INSERT INTO [TabContentVisualBlock] ([TabContentId], [VisualBlockId])
                   VALUES (@TabContentId, @VisualBlockId);
                   SELECT last_insert_rowid()",
                new
                {
                    TabContentId = tabContentId,
                    VisualBlockId = visualBlockId
                });

            return await Repository.QuerySingleAsync<int>(query);
        }
    }
}
