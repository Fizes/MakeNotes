using System.Collections.Generic;
using MakeNotes.Common.Core.Requests;
using MakeNotes.DAL.Models;

namespace MakeNotes.Notebook.Core.Queries
{
    public class GetAllTabs : IRequest<IEnumerable<Tab>>
    {
    }
}
