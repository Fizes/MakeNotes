using System.Threading.Tasks;
using MakeNotes.Common.Core.Factories;

namespace MakeNotes.Common.Core.Notifications
{
    /// <summary>
    /// Executes notification handlers in background.
    /// </summary>
    public class FireAndForgetNotificationStrategy : INotificationStrategy
    {
        private readonly INotificationStrategyFactory _notificationStrategyFactory;

        public FireAndForgetNotificationStrategy(INotificationStrategyFactory notificationStrategyFactory)
        {
            _notificationStrategyFactory = notificationStrategyFactory;
        }

        public void Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            var task = new Task(() =>
            {
                var defaultNotificationStrategy = _notificationStrategyFactory.Create<DefaultNotificationStrategy>();
                defaultNotificationStrategy.Publish(notification);
            });

            task.Start();
        }
    }
}
