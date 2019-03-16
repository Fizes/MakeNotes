using System.Threading.Tasks;

namespace MakeNotes.Common.Core.Requests
{
    /// <summary>
    /// Defines a handler for a request.
    /// </summary>
    /// <typeparam name="TRequest">The type of request being handled.</typeparam>
    /// <typeparam name="TResponse">The type of response from the handler.</typeparam>
    public interface IRequestHandler<in TRequest, TResponse> : IHandler
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Executes a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from the request.</returns>
        Task<TResponse> ExecuteAsync(TRequest request);
    }

    /// <summary>
    /// Defines a handler for a request with a void (<see cref="Unit" />) response.
    /// </summary>
    /// <typeparam name="TRequest">The type of request being handled.</typeparam>
    public interface IRequestHandler<in TRequest> : IRequestHandler<TRequest, Unit>
        where TRequest : IRequest<Unit>
    {
    }
}
