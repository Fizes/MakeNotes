using System.Threading.Tasks;
using MakeNotes.Common.Core;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Core;
using MakeNotes.Notebook.Core.Notifications;

namespace MakeNotes.Notebook.Core.Commands
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
            var query = new QueryObject(
                @"INSERT INTO [Tab] ([Name], [Order])
                  VALUES (@Name, @Order);
                  SELECT last_insert_rowid()", command);

            var newId = await _repository.QuerySingleAsync<long>(query);

            return (int)newId;
        }

        public async Task<Unit> ExecuteAsync(DeleteTab command)
        {
            var query = new QueryObject(
                @"DELETE FROM [Tab]
                  WHERE [Id] = @Id", command);

            await _repository.ExecuteAsync(query);

            ApplicationEvents.Raise(new TabDeleted(command.Id));

            return await Unit.Task;
        }
    }
}
