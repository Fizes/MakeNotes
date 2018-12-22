using System.Data;

namespace MakeNotes.DAL.Infrastructure
{
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Returns a new instance of db connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection Create();
    }
}