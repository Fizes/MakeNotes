using System.Threading.Tasks;

namespace MakeNotes.Common.Core.Queries
{
    /// <summary>
    /// Represents a handler that executes a query returning the specified result. Doesn't modify a state of the system.
    /// </summary>
    /// <typeparam name="TQuery">Query type.</typeparam>
    /// <typeparam name="TResult">Query result type.</typeparam>
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">Query instance.</param>
        /// <returns>Query result.</returns>
        TResult Execute(TQuery query);
    }
    
    /// <summary>
    /// Represents an async handler that executes a query returning the specified result. Doesn't modify a state of the system.
    /// </summary>
    /// <typeparam name="TQuery">Query type.</typeparam>
    /// <typeparam name="TResult">Query result type.</typeparam>
    public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">Query instance.</param>
        /// <returns>Query result.</returns>
        Task<TResult> ExecuteAsync(TQuery query);
    }
}
