using MakeNotes.Common.Core.Queries;
using MakeNotes.DAL.Models;

namespace MakeNotes.Notebook.Core.Queries
{
    public class FindTabById : IQuery<Tab>
    {
        public FindTabById(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
