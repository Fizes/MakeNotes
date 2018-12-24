using System.Data;

namespace MakeNotes.DAL.Infrastructure
{
    /// <summary>
    /// <see cref="IDbConnection"/> factory.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Returns a new instance of <see cref="IDbConnection"/>.
        /// </summary>
        /// <returns></returns>
        IDbConnection Create();
    }
}