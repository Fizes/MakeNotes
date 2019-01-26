using System.Data;
using System.Data.SQLite;
using Dapper.AmbientContext;

namespace MakeNotes.DAL.Infrastructure
{
    /// <summary>
    /// <see cref="IDbConnection"/> factory.
    /// </summary>
    public class DefaultConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DefaultConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Returns a new instance of <see cref="IDbConnection"/>.
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}
