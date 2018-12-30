namespace MakeNotes.DAL.Core
{
    /// <summary>
    /// Encapsulates sql text and its parameters.
    /// </summary>
    public class QueryObject
    {
        public QueryObject(string sql, object queryParams = null)
        {
            Sql = sql;
            Parameters = queryParams;
        }

        /// <summary>
        /// Query text.
        /// </summary>
        public string Sql { get; }

        /// <summary>
        /// Query parameters.
        /// </summary>
        public object Parameters { get; }
    }
}
