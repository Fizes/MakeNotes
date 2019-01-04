using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Notebook.Core.Commands
{
    public class CreateTab : IRequest<int>
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
