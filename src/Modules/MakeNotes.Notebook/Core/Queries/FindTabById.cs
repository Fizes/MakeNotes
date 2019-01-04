using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Models;

namespace MakeNotes.Notebook.Core.Queries
{
    public class FindTabById : IRequest<Tab>
    {
        public FindTabById(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
