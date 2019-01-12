using System.Threading.Tasks;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Core;

namespace MakeNotes.Notebook.Core.Commands.Handlers
{
    public class TabContentCommandHandler : IRequestHandler<AddTabContent, int>
    {
        private readonly IRepository _repository;

        public TabContentCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> ExecuteAsync(AddTabContent command)
        {
            var query = new QueryObject(
                @"SELECT [Id]
                  FROM [VisualBlockType]
                  WHERE [SysName] = @SysName", new { SysName = command.Type });

            var visualBlockTypeId = await _repository.QuerySingleAsync<int>(query);

            query = new QueryObject(
                @"INSERT INTO [TabContent] ([TabId], [Order], [VisualBlockTypeId])
                  VALUES (@TabId, @Order, @VisualBlockTypeId);
                  SELECT last_insert_rowid()",
                new
                {
                    command.TabId,
                    command.Order,
                    VisualBlockTypeId = visualBlockTypeId
                });

            var newId = await _repository.QuerySingleAsync<long>(query);

            return (int)newId;
        }
    }
}
