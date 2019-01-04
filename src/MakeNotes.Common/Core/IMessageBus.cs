using System.Threading.Tasks;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Represents a mediator between top-level code and code that does a particular action.
    /// Hides any specific details about code in lower level.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Sends the request to a particular handler.
        /// </summary>
        /// <param name="command">Command instance.</param>
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);

        /// <summary>
        /// Publishes all pending notifications.
        /// </summary>
        void Publish();

        /// <summary>
        /// Publishes the specified notification directly.
        /// </summary>
        /// <typeparam name="TNotification">Notification type.</typeparam>
        /// <param name="notification">Notification instance.</param>
        void Publish<TNotification>(TNotification notification) where TNotification : INotification;
    }
}
