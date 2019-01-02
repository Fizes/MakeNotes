using MakeNotes.Common.Core.Commands;

namespace MakeNotes.Notebook.Core.Commands
{
    public class CreateTab : ICommand
    {
        public CreateTab(string name, int order)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; }
        public int Order { get; }
    }
}
