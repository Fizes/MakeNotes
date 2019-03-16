using System;
using System.Threading.Tasks;
using MakeNotes.Common.Core;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Queries;
using MakeNotes.Notebook.Core.Notifications;

namespace MakeNotes.Notebook.Core.Commands.Handlers
{
    public class TabCommandHandler : IRequestHandler<CreateTab, int>,
                                     IRequestHandler<DeleteTab>
    {
        private readonly IRepository _repository;

        public TabCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> ExecuteAsync(CreateTab command)
        {
            var query = new QueryObject(String.Concat(TabQueries.CreateTab, CommonQueries.GetInsertedId), command);

            var newId = await _repository.QuerySingleAsync<long>(query);

            return (int)newId;
        }

        public async Task<Unit> ExecuteAsync(DeleteTab command)
        {
            var query = new QueryObject(TabQueries.DeleteTab, command);

            await _repository.ExecuteAsync(query);

            ApplicationEvents.Raise(new TabDeleted(command.Id));

            return Unit.Value;
        }
    }
}
