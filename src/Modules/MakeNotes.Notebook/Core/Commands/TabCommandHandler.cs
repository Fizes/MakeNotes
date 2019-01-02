using System.Threading.Tasks;
using MakeNotes.Common.Core;
using MakeNotes.Common.Core.Commands;
using MakeNotes.DAL.Core;
using MakeNotes.Notebook.Core.Notifications;

namespace MakeNotes.Notebook.Core.Commands
{
    public class TabCommandHandler : ICommandHandler<CreateTab>,
                                     ICommandHandler<DeleteTab>
    {
        private readonly IRepository _repository;

        public TabCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Task ExecuteAsync(CreateTab command)
        {
            var query = new QueryObject(
                @"INSERT INTO [Tab] ([Name], [Order])
                  VALUES (@Name, @Order)", command);

            return _repository.ExecuteAsync(query);
        }

        public async Task ExecuteAsync(DeleteTab command)
        {
            var query = new QueryObject(
                @"DELETE FROM [Tab]
                  WHERE [Id] = @Id", command);

            await _repository.ExecuteAsync(query);

            ApplicationEvents.Raise(new TabDeleted(command.Id));
        }
    }
}
