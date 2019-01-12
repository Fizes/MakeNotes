using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Models;

namespace MakeNotes.Notebook.Core.Queries
{
    public class FindVisualBlockTypeById : IRequest<VisualBlockType>
    {
        public FindVisualBlockTypeById(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
