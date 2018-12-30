using System.Threading.Tasks;
using MakeNotes.Common.Core.Commands;
using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Represents a mediator between top-level code and code that does a particular action.
    /// Hides any specific details about code in lower level.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Sends the command to a particular handler which executes the command.
        /// </summary>
        /// <param name="command">Command instance.</param>
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;

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
