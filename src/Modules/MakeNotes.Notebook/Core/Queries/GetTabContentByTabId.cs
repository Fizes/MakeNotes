using System.Collections.Generic;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Models;

namespace MakeNotes.Notebook.Core.Queries
{
    public class GetTabContentByTabId : IRequest<IEnumerable<TabContent>>
    {
        public GetTabContentByTabId(int tabId)
        {
            TabId = tabId;
        }

        public int TabId { get; }
    }
}
