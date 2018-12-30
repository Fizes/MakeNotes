using System.Threading.Tasks;
using MakeNotes.Common.Core.Commands;
using MakeNotes.DAL.Core;

namespace MakeNotes.Notebook.Core.Commands
{
    public class TabCommandHandler : ICommandHandler<CreateTab>
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
    }
}
