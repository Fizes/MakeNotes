using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeNotes.DAL.Core
{
    /// <summary>
    /// Represents a set of methods to work with the database.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Executes a query that returns no result.
        /// </summary>
        /// <param name="queryObject">Data required for a query.</param>
        /// <returns></returns>
        Task ExecuteAsync(QueryObject queryObject);

        /// <summary>
        /// Executes a query that returns collection of elements with the specified type.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="queryObject">Data required for a query.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(QueryObject queryObject);

        /// <summary>
        /// Executes a query that returns first element with the specified type from the resulting set.
        /// Throws an exception if no element found.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="queryObject">Data required for a query.</param>
        /// <returns></returns>
        Task<T> QueryFirstAsync<T>(QueryObject queryObject);

        /// <summary>
        /// Executes a query that returns first element with the specified type from the resulting set.
        /// Returns null if no element found.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="queryObject">Data required for a query.</param>
        /// <returns></returns>
        Task<T> QueryFirstOrDefaultAsync<T>(QueryObject queryObject);

        /// <summary>
        /// Executes a query that returns a single element with the specified type from the resulting set.
        /// Throws an exception if the element is not found or the resulting set contains more than one such element.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="queryObject">Data required for a query.</param>
        /// <returns></returns>
        Task<T> QuerySingleAsync<T>(QueryObject queryObject);

        /// <summary>
        /// Executes a query that returns a single element with the specified type, or null if not found, from the resulting set.
        /// Throws an exception if the resulting set contains more than one such element.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="queryObject">Data required for a query.</param>
        /// <returns></returns>
        Task<T> QuerySingleOrDefaultAsync<T>(QueryObject queryObject);
    }
}