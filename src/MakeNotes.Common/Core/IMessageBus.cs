using MakeNotes.Common.Core.Commands;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Core.Queries;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Represents a mediator between top-level code and code that does a particular action.
    /// Hides any specific details about code in lower level.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Sends a command to a particular handler which executes the command.
        /// </summary>
        /// <param name="command">Command instance.</param>
        void Send(ICommand command);

        /// <summary>
        /// Sends a query to a particular handler which executes the query and returns its result.
        /// </summary>
        /// <typeparam name="TResult">Query result type.</typeparam>
        /// <param name="query">Query instance.</param>
        /// <returns></returns>
        TResult Send<TResult>(IQuery<TResult> query);

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
