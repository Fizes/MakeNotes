using System.Collections.Generic;
using System.Threading.Tasks;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Models;
using MakeNotes.DAL.Queries;

namespace MakeNotes.Notebook.Core.Queries.Handlers
{
    public class TabContentQueryHandler : IRequestHandler<GetTabContentByTabId, IEnumerable<TabContent>>,
                                          IRequestHandler<GetLastTabContentOrder, int>,
                                          IRequestHandler<GetCountOfTabContentByTabId, int>
    {
        private readonly IRepository _repository;

        public TabContentQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TabContent>> ExecuteAsync(GetTabContentByTabId query)
        {
            var queryObject = new QueryObject(TabContentQueries.GetTabContentByTabId, query);
            return _repository.QueryAsync<TabContent>(queryObject);
        }

        public Task<int> ExecuteAsync(GetLastTabContentOrder query)
        {
            var queryObject = new QueryObject(
                @"SELECT COALESCE(MAX([Order]), -1)
                  FROM [TabContent]
                  WHERE [TabId] = @TabId", query);

            return _repository.QuerySingleAsync<int>(queryObject);
        }

        public Task<int> ExecuteAsync(GetCountOfTabContentByTabId query)
        {
            var queryObject = new QueryObject(
                @"SELECT COUNT([Id])
                  FROM [TabContent]
                  WHERE [TabId] = @TabId", query);

            return _repository.QuerySingleAsync<int>(queryObject);
        }
    }
}
