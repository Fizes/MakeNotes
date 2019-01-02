using MakeNotes.Common.Core.Commands;

namespace MakeNotes.Notebook.Core.Commands
{
    public class DeleteTab : ICommand
    {
        public DeleteTab(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
