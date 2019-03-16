using System.Collections.Generic;
using MakeNotes.Common.Core.Requests;
using MakeNotes.Notebook.Templates.VisualBlocks.Models;

namespace MakeNotes.Notebook.Core.Queries
{
    public class GetPasswordSheetsByTabContentId : IRequest<IEnumerable<PasswordSheetDto>>
    {
        public GetPasswordSheetsByTabContentId(int tabContentId)
        {
            TabContentId = tabContentId;
        }

        public int TabContentId { get; }
    }
}
