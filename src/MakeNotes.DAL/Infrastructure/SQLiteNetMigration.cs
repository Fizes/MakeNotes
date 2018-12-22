using System.Data.SQLite;
using Dapper;
using SimpleMigrations;

namespace MakeNotes.DAL.Infrastructure
{
    /// <summary>
    /// Migration base class to execute queries using sqlite.
    /// </summary>
    public abstract class SQLiteNetMigration : IMigration<SQLiteConnection>
    {
        protected SQLiteConnection Connection { get; set; }

        protected IMigrationLogger Logger { get; set; }

        protected bool UseTransaction { get; set; }

        public abstract void Up();
        
        public abstract void Down();
        
        public void Execute(string sql)
        {
            Logger.LogSql(sql);
            Connection.Execute(sql);
        }

        private void ApplyMigration(MigrationDirection direction)
        {
            if (direction == MigrationDirection.Up)
            {
                Up();
            }
            else
            {
                Down();
            }
        }

        void IMigration<SQLiteConnection>.RunMigration(MigrationRunData<SQLiteConnection> data)
        {
            Connection = data.Connection;
            Logger = data.Logger;

            if (UseTransaction)
            {
                using (var transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        ApplyMigration(data.Direction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            else
            {
                ApplyMigration(data.Direction);
            }
        }
    }
}
