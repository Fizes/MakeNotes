using System.Data;

namespace MakeNotes.DAL.Infrastructure.Extensions
{
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// Begins a database transaction. Ensures that the connection is open before beginning the transaction.
        /// </summary>
        /// <param name="connection"><see cref="IDbConnection"/> instance.</param>
        /// <param name="isolationLevel">Optional isolation level.</param>
        /// <returns></returns>
        public static IDbTransaction BeginTransactionSafe(this IDbConnection connection, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection.BeginTransaction(isolationLevel);
        }
    }
}
