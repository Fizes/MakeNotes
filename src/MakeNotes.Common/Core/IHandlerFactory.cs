using MakeNotes.Common.Core.Commands;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Core.Queries;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Creates instances of <see cref="IQueryHandler{TQuery, TResult}"/>,
    /// <see cref="ICommandHandler{TCommand}"/> and <see cref="INotificationHandler{TNotification}"/> handlers.
    /// </summary>
    public interface IHandlerFactory
    {
        /// <summary>
        /// Creates an instance of a query handler of the specified query type.
        /// </summary>
        /// <typeparam name="TResult">Query result type.</typeparam>
        /// <param name="query">Query handler parameter.</param>
        /// <returns></returns>
        IQueryHandler<IQuery<TResult>, TResult> Create<TResult>(IQuery<TResult> query);

        /// <summary>
        /// Creates an instance of a command handler of the specified command type.
        /// </summary>
        /// <param name="command">Command handler parameter.</param>
        /// <returns></returns>
        ICommandHandler<ICommand> Create(ICommand command);

        /// <summary>
        /// Creates an instance of a notification handler of the specified notification type.
        /// </summary>
        /// <param name="notification">Notification handler parameter.</param>
        /// <returns></returns>
        INotificationHandler<INotification> Create(INotification notification);
    }
}
