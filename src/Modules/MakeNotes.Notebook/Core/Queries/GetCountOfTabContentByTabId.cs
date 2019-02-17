using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Notebook.Core.Queries
{
    public class GetCountOfTabContentByTabId : IRequest<int>
    {
        public GetCountOfTabContentByTabId(int tabId)
        {
            TabId = tabId;
        }

        public int TabId { get; }
    }
}
