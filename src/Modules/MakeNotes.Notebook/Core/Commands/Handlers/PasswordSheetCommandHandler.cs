using System.Threading.Tasks;
using MakeNotes.Common.Core;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Core;
using MakeNotes.Notebook.Providers;
using MakeNotes.Notebook.Templates.VisualBlocks.Models;

namespace MakeNotes.Notebook.Core.Commands.Handlers
{
    public class PasswordSheetCommandHandler : IRequestHandler<AddPasswordSheet, int>,
                                               IRequestHandler<UpdatePasswordSheet>,
                                               IRequestHandler<DeletePasswordSheet>
    {
        private readonly PasswordSheetVisualBlockProvider _passwordSheetProvider;
        private readonly IRepository _repository;

        public PasswordSheetCommandHandler(PasswordSheetVisualBlockProvider passwordSheetProvider, IRepository repository)
        {
            _passwordSheetProvider = passwordSheetProvider;
            _repository = repository;
        }

        public Task<int> ExecuteAsync(AddPasswordSheet command)
        {
            var passwordSheet = new PasswordSheetDto
            {
                Site = command.Site,
                Username = command.Username,
                Password = command.Password,
                Description = command.Description
            };
            return _passwordSheetProvider.CreateVisualBlockAsync(command.TabContentId, passwordSheet);
        }

        public async Task<Unit> ExecuteAsync(UpdatePasswordSheet command)
        {
            var passwordSheet = new PasswordSheetDto
            {
                Id = command.Id,
                Site = command.Site,
                Username = command.Username,
                Password = command.Password,
                Description = command.Description
            };
            await _passwordSheetProvider.UpdateVisualBlockAsync(passwordSheet);

            return Unit.Value;
        }

        public async Task<Unit> ExecuteAsync(DeletePasswordSheet command)
        {
            var query = new QueryObject(
                @"DELETE FROM [TabContentVisualBlock]
                  WHERE [TabContentId] = @TabContentId AND [VisualBlockId] = @Id;", command);

            await _repository.ExecuteAsync(query);

            query = new QueryObject(
                @"DELETE FROM [PasswordSheet]
                  WHERE [Id] = @Id;", command);

            await _repository.ExecuteAsync(query);

            return Unit.Value;
        }
    }
}
