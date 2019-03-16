using System.Collections.Generic;
using System.Threading.Tasks;
using MakeNotes.Common.Core.Requests;
using MakeNotes.Notebook.Providers;
using MakeNotes.Notebook.Templates.VisualBlocks.Models;

namespace MakeNotes.Notebook.Core.Queries.Handlers
{
    public class PasswordSheetQueryHandler : IRequestHandler<GetPasswordSheetsByTabContentId, IEnumerable<PasswordSheetDto>>
    {
        private readonly PasswordSheetVisualBlockProvider _passwordSheetProvider;

        public PasswordSheetQueryHandler(PasswordSheetVisualBlockProvider passwordSheetProvider)
        {
            _passwordSheetProvider = passwordSheetProvider;
        }

        public Task<IEnumerable<PasswordSheetDto>> ExecuteAsync(GetPasswordSheetsByTabContentId request)
        {
            return _passwordSheetProvider.GetVisualBlocksAsync(request.TabContentId);
        }
    }
}
