namespace MakeNotes.Common.Core.Queries
{
    /// <summary>
    /// Marks a class as a query.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    public interface IQuery<out TResult>
    {
    }
}
