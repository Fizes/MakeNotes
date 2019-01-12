using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Notebook.Core.Commands
{
    public class AddTabContent : IRequest<int>
    {
        public AddTabContent(int tabId, int order, string type)
        {
            TabId = tabId;
            Order = order;
            Type = type;
        }

        public int TabId { get; }
        public int Order { get; }
        public string Type { get; }
    }
}
