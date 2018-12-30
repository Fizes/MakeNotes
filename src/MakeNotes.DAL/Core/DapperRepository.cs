using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace MakeNotes.DAL.Core
{
    public class DapperRepository : IRepository
    {
        private readonly IDbConnection _connection;

        public DapperRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(QueryObject queryObject, IDbTransaction transaction = null)
        {
            return await _connection.QueryAsync<T>(queryObject.Sql, queryObject.Parameters, transaction);
        }

        public async Task<T> QuerySingleAsync<T>(QueryObject queryObject, IDbTransaction transaction = null)
        {
            return await _connection.QuerySingleAsync<T>(queryObject.Sql, queryObject.Parameters, transaction);
        }
        
        public async Task<T> QuerySingleOrDefaultAsync<T>(QueryObject queryObject, IDbTransaction transaction = null)
        {
            return await _connection.QuerySingleOrDefaultAsync<T>(queryObject.Sql, queryObject.Parameters, transaction);
        }

        public async Task<T> QueryFirstAsync<T>(QueryObject queryObject, IDbTransaction transaction = null)
        {
            return await _connection.QueryFirstAsync<T>(queryObject.Sql, queryObject.Parameters, transaction);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(QueryObject queryObject, IDbTransaction transaction = null)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(queryObject.Sql, queryObject.Parameters, transaction);
        }

        public async Task ExecuteAsync(QueryObject queryObject, IDbTransaction transaction = null)
        {
            await _connection.ExecuteAsync(queryObject.Sql, queryObject.Parameters, transaction);
        }

        public IDbTransaction BeginTransaction()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            return _connection.BeginTransaction();
        }
    }
}
