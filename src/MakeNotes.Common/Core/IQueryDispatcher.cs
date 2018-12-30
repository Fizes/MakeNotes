using System.Threading.Tasks;
using MakeNotes.Common.Core.Queries;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Dispatches queries to appropriate handlers.
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Sends the query to a particular handler which executes the query.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}
