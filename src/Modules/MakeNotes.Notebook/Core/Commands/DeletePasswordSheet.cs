using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Notebook.Core.Commands
{
    public class DeletePasswordSheet : IRequest
    {
        public DeletePasswordSheet(int id, int tabContentId)
        {
            Id = id;
            TabContentId = tabContentId;
        }

        public int Id { get; }
        public int TabContentId { get; }
    }
}
