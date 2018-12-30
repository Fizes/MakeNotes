using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MakeNotes.DAL.Core
{
    /// <summary>
    /// Represents a set of methods to work with the database.
    /// </summary>
    public interface IRepository
    {
        IDbTransaction BeginTransaction();

        Task ExecuteAsync(QueryObject queryObject, IDbTransaction transaction = null);
        
        Task<IEnumerable<T>> QueryAsync<T>(QueryObject queryObject, IDbTransaction transaction = null);
        
        Task<T> QueryFirstAsync<T>(QueryObject queryObject, IDbTransaction transaction = null);

        Task<T> QueryFirstOrDefaultAsync<T>(QueryObject queryObject, IDbTransaction transaction = null);

        Task<T> QuerySingleAsync<T>(QueryObject queryObject, IDbTransaction transaction = null);

        Task<T> QuerySingleOrDefaultAsync<T>(QueryObject queryObject, IDbTransaction transaction = null);
    }
}