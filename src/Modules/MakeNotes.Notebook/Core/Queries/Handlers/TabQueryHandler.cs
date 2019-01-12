using System.Collections.Generic;
using System.Threading.Tasks;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Models;

namespace MakeNotes.Notebook.Core.Queries.Handlers
{
    public class TabQueryHandler : IRequestHandler<GetAllTabs, IEnumerable<Tab>>,
                                   IRequestHandler<FindTabById, Tab>,
                                   IRequestHandler<GetLastTabOrder, int>
    {
        private readonly IRepository _repository;

        public TabQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Tab>> ExecuteAsync(GetAllTabs query)
        {
            var queryObject = new QueryObject(
                @"SELECT [Id], [Name], [Order]
                  FROM [Tab]
                  ORDER BY [Order]");

            return _repository.QueryAsync<Tab>(queryObject);
        }

        public Task<Tab> ExecuteAsync(FindTabById query)
        {
            var queryObject = new QueryObject(
                @"SELECT [Id], [Name], [Order]
                  FROM [Tab]
                  WHERE [Id] = @Id", query);

            return _repository.QuerySingleOrDefaultAsync<Tab>(queryObject);
        }

        public Task<int> ExecuteAsync(GetLastTabOrder query)
        {
            var queryObject = new QueryObject(
                @"SELECT COALESCE(MAX([Order]), 0)
                  FROM [Tab]");

            return _repository.QuerySingleAsync<int>(queryObject);
        }
    }
}
