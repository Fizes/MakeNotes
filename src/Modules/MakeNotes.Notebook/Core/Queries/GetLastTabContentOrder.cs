using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Notebook.Core.Queries
{
    public class GetLastTabContentOrder : IRequest<int>
    {
        public GetLastTabContentOrder(int tabId)
        {
            TabId = tabId;
        }

        public int TabId { get; }
    }
}
