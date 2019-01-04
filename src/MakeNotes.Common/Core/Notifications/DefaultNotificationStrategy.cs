using MakeNotes.Common.Core.Factories;

namespace MakeNotes.Common.Core.Notifications
{
    /// <summary>
    /// Executes notification handlers synchronously.
    /// </summary>
    public class DefaultNotificationStrategy : INotificationStrategy
    {
        private readonly IHandlerFactory _handlerFactory;

        public DefaultNotificationStrategy(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
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
