using System.Data;
using System.Data.SQLite;

namespace MakeNotes.DAL.Infrastructure
{
    public class DefaultConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DefaultConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Create()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}
