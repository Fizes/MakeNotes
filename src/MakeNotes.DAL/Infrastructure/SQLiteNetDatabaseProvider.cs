using System;
using System.Data.SQLite;
using Dapper;
using MakeNotes.DAL.Models;
using SimpleMigrations;

namespace MakeNotes.DAL.Infrastructure
{
    /// <summary>
    /// Implementation of <see cref="IDatabaseProvider{T}"/> to work with the 'SchemaVersion' table in sqlite database.
    /// </summary>
    public class SQLiteNetDatabaseProvider : IDatabaseProvider<SQLiteConnection>
    {
        private readonly SQLiteConnection _connection;

        public SQLiteNetDatabaseProvider(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public SQLiteConnection BeginOperation()
        {
            return _connection;
        }

        public void EndOperation()
        {
        }

        public long EnsurePrerequisitesCreatedAndGetCurrentVersion()
        {
            _connection.Execute(
                @"CREATE TABLE IF NOT EXISTS [SchemaVersion]
                  (
                      [Id] INTEGER PRIMARY KEY,
                      [Version] INTEGER NOT NULL,
                      [AppliedOn] DATETIME NOT NULL,
                      [Description] TEXT NOT NULL
                  )");

            return GetCurrentVersion();
        }

        public long GetCurrentVersion()
        {
            // Return 0 if the table has no entries
            var latestOrNull = _connection.QueryFirstOrDefault<SchemaVersion>(
                @"SELECT [Id], [Version], [AppliedOn], [Description]
                  FROM [SchemaVersion]
                  ORDER BY [Id] DESC;");

            return latestOrNull?.Version ?? 0;
        }

        public void UpdateVersion(long oldVersion, long newVersion, string newDescription)
        {
            var values = new SchemaVersion
            {
                Version = newVersion,
                AppliedOn = DateTime.UtcNow,
                Description = newDescription
            };

            _connection.Execute(
                @"INSERT INTO [SchemaVersion] ([Version], [AppliedOn], [Description])
                  VALUES (@Version, @AppliedOn, @Description)", values);
        }
    }
}
