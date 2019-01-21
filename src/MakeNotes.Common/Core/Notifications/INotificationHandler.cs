namespace MakeNotes.Common.Core.Notifications
{
    /// <summary>
    /// Represents a handler that is executed after an event occurred.
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface INotificationHandler<in TNotification> : IHandler
        where TNotification : INotification
    {
        /// <summary>
        /// Executes actions in response to a notification.
        /// </summary>
        /// <param name="notification">Notification instance.</param>
        void Handle(TNotification notification);
    }
}
