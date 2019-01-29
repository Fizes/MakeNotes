using System.Collections.Generic;
using System.Threading.Tasks;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Models;
using MakeNotes.DAL.Queries;

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
            var queryObject = new QueryObject(TabQueries.GetAllTabs);

            return _repository.QueryAsync<Tab>(queryObject);
        }

        public Task<Tab> ExecuteAsync(FindTabById query)
        {
            var queryObject = new QueryObject(TabQueries.FindTabById, query);

            return _repository.QuerySingleOrDefaultAsync<Tab>(queryObject);
        }

        public Task<int> ExecuteAsync(GetLastTabOrder query)
        {
            var queryObject = new QueryObject(TabQueries.GetLastTabOrder);

            return _repository.QuerySingleAsync<int>(queryObject);
        }
    }
}
