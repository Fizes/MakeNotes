using System.Reflection;
using System.Threading.Tasks;
using MakeNotes.Common.Core.Queries;

namespace MakeNotes.Common.Core
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IHandlerFactory _handlerFactory;

        private static readonly MethodInfo ExecuteMethod;

        static QueryDispatcher()
        {
            ExecuteMethod = typeof(QueryDispatcher).GetMethod(nameof(QueryDispatcher.ExecuteCoreAsync), BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public QueryDispatcher(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            // A little reflection hack to call a generic method with multiple parameters
            // via a single parameter generic method
            var queryType = query.GetType();
            var method = ExecuteMethod.MakeGenericMethod(queryType, typeof(TResult));

            return (Task<TResult>)method.Invoke(this, new[] { query });
        }

        private Task<TResult> ExecuteCoreAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _handlerFactory.Create<IQueryHandler<TQuery, TResult>>();
            return handler.ExecuteAsync(query);
        }
    }
}
