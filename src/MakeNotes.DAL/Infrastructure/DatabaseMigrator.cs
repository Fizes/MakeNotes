using System.Data.SQLite;
using SimpleMigrations;

namespace MakeNotes.DAL.Infrastructure
{
    public static class DatabaseMigrator
    {
        /// <summary>
        /// Migrates the database up to the latest version.
        /// </summary>
        /// <param name="dbConnectionFactory">Factory to create a db connection instance.</param>
        public static void Migrate(IDbConnectionFactory dbConnectionFactory)
        {
            var migrationsAssembly = typeof(DatabaseMigrator).Assembly;
            using (var connection = dbConnectionFactory.Create())
            {
                var databaseProvider = new SQLiteNetDatabaseProvider((SQLiteConnection)connection);

                var migrator = new SimpleMigrator<SQLiteConnection, SQLiteNetMigration>(migrationsAssembly, databaseProvider);
                migrator.Load();

                migrator.MigrateToLatest();
            }
        }
    }
}
