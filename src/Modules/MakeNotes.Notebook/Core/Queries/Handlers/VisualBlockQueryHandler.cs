using System.Threading.Tasks;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Models;

namespace MakeNotes.Notebook.Core.Queries.Handlers
{
    public class VisualBlockQueryHandler : IRequestHandler<FindVisualBlockTypeById, VisualBlockType>
    {
        private readonly IRepository _repository;

        public VisualBlockQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Task<VisualBlockType> ExecuteAsync(FindVisualBlockTypeById query)
        {
            var queryObject = new QueryObject(
                @"SELECT [Id], [SysName]
                  FROM [VisualBlockType]
                  WHERE [Id] = @Id", query);

            return _repository.QuerySingleAsync<VisualBlockType>(queryObject);
        }
    }
}
