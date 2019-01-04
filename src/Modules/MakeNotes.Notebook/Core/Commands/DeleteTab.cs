using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Notebook.Core.Commands
{
    public class DeleteTab : IRequest
    {
        public DeleteTab(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
