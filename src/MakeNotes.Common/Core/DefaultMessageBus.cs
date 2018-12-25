using MakeNotes.Common.Core.Commands;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Core.Queries;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Default implementation of <see cref="IMessageBus"/> that executes actions synchronously in-memory.
    /// </summary>
    public class DefaultMessageBus : IMessageBus
    {
        private readonly IHandlerFactory _handlerFactory;

        public DefaultMessageBus(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public void Send(ICommand command)
        {
            var handler = _handlerFactory.Create(command);
            handler.Execute(command);
        }

        public TResult Send<TResult>(IQuery<TResult> query)
        {
            var handler = _handlerFactory.Create(query);
            var result = handler.Execute(query);

            return result;
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
            var handler = _handlerFactory.Create(notification);
            handler.Handle(notification);
        }
    }
}
