using System.Data;
using System.Data.SQLite;
using System.IO;
using MakeNotes.Common.Infrastructure.Extensions;
using SimpleMigrations;

namespace MakeNotes.DAL.Infrastructure
{
    public static class DatabaseMigrator
    {
        /// <summary>
        /// Migrates the database up to the latest version.
        /// </summary>
        /// <param name="dbConnectionFactory">Factory to create a <see cref="IDbConnection"/> instance.</param>
        public static void Migrate(IDbConnectionFactory dbConnectionFactory)
        {
            var migrationsAssembly = typeof(DatabaseMigrator).Assembly;
            using (var connection = dbConnectionFactory.Create())
            {
                EnsureDatabaseDirectoryExists(connection.ConnectionString);

                var databaseProvider = new SQLiteNetDatabaseProvider((SQLiteConnection)connection);
                var migrator = new SimpleMigrator<SQLiteConnection, SQLiteNetMigration>(migrationsAssembly, databaseProvider);
                migrator.Load();

                if (migrator.CurrentMigration.Version != migrator.LatestMigration.Version)
                {
                    migrator.MigrateToLatest();
                }
            }
        }

        private static void EnsureDatabaseDirectoryExists(string connectionString)
        {
            var values = connectionString.ToNameValueCollection();
            var dbFile = values.Get(SQLiteConnectionStringKeys.DataSource);
            Directory.CreateDirectory(Path.GetDirectoryName(dbFile));
        }
    }
}
