using MakeNotes.Common.Core.Queries;
using MakeNotes.DAL.Models;

namespace MakeNotes.Notebook.Core.Queries
{
    public class FindTabById : IQuery<Tab>
    {
        public int Id { get; set; }
    }
}
