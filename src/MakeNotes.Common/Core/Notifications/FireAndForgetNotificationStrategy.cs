using MakeNotes.Common.Core.Factories;
using MakeNotes.Common.Interfaces;

namespace MakeNotes.Common.Core.Notifications
{
    /// <summary>
    /// Executes notification handlers in background.
    /// </summary>
    public class FireAndForgetNotificationStrategy : INotificationStrategy
    {
        private readonly IBackgroundTask _backgroundTask;
        private readonly INotificationStrategyFactory _notificationStrategyFactory;

        public FireAndForgetNotificationStrategy(IBackgroundTask backgroundTask, INotificationStrategyFactory notificationStrategyFactory)
        {
            _backgroundTask = backgroundTask;
            _notificationStrategyFactory = notificationStrategyFactory;
        }

        public void Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            _backgroundTask.Start(() =>
            {
                var defaultNotificationStrategy = _notificationStrategyFactory.Create<DefaultNotificationStrategy>();
                defaultNotificationStrategy.Publish(notification);
            });
        }
    }
}
