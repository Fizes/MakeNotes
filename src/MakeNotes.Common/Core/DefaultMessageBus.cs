using System.Threading.Tasks;
using MakeNotes.Common.Core.Commands;
using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Default implementation of <see cref="IMessageBus"/> that executes actions in-memory.
    /// </summary>
    public class DefaultMessageBus : IMessageBus
    {
        private readonly IHandlerFactory _handlerFactory;

        public DefaultMessageBus(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _handlerFactory.Create<ICommandHandler<TCommand>>();
            return handler.ExecuteAsync(command);
        }

        public void Publish()
        {
            while (ApplicationEvents.Notifications.TryDequeue(out var notification))
            {
                Publish(notification);
            }
        }

        public void Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            var handlers = _handlerFactory.CreateAll<INotificationHandler<TNotification>>();
            foreach (var handler in handlers)
            {
                handler.Handle(notification);
            }
        }
    }
}
