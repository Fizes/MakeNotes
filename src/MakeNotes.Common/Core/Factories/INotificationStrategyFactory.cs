using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Common.Core.Factories
{
    public interface INotificationStrategyFactory
    {
        /// <summary>
        /// Creates a new instance of the specified <see cref="INotificationStrategy"/>.
        /// </summary>
        /// <typeparam name="TNotificationStrategy"></typeparam>
        /// <returns></returns>
        TNotificationStrategy Create<TNotificationStrategy>() where TNotificationStrategy : INotificationStrategy;
    }
}
