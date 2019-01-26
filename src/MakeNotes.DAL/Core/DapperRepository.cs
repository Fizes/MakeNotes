using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.AmbientContext;

namespace MakeNotes.DAL.Core
{
    public class DapperRepository : IRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;
        private readonly IAmbientDbContextFactory _ambientDbContextFactory;

        public DapperRepository(IAmbientDbContextLocator ambientDbContextLocator, IAmbientDbContextFactory ambientDbContextFactory)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
            _ambientDbContextFactory = ambientDbContextFactory;
        }

        private IAmbientDbContextQueryProxy Context => _ambientDbContextLocator.Get();

        public Task<IEnumerable<T>> QueryAsync<T>(QueryObject queryObject)
        {
            return UsingContext(() => Context.QueryAsync<T>(queryObject.Sql, queryObject.Parameters));
        }

        public Task<T> QuerySingleAsync<T>(QueryObject queryObject)
        {
            return UsingContext(() => Context.QuerySingleAsync<T>(queryObject.Sql, queryObject.Parameters));
        }
        
        public Task<T> QuerySingleOrDefaultAsync<T>(QueryObject queryObject)
        {
            return UsingContext(() => Context.QuerySingleOrDefaultAsync<T>(queryObject.Sql, queryObject.Parameters));
        }

        public Task<T> QueryFirstAsync<T>(QueryObject queryObject)
        {
            return UsingContext(() => Context.QueryFirstAsync<T>(queryObject.Sql, queryObject.Parameters));
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(QueryObject queryObject)
        {
            return UsingContext(() => Context.QueryFirstOrDefaultAsync<T>(queryObject.Sql, queryObject.Parameters));
        }

        public Task ExecuteAsync(QueryObject queryObject)
        {
            return UsingContext(() => Context.ExecuteAsync(queryObject.Sql, queryObject.Parameters));
        }

        private Task<T> UsingContext<T>(Func<Task<T>> dbQuery)
        {
            // Join to parent context if it exists
            using (var context = _ambientDbContextFactory.Create(join: true, suppress: true))
            {
                return dbQuery();
            }
        }
    }
}
