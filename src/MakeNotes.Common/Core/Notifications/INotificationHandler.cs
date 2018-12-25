namespace MakeNotes.Common.Core.Notifications
{
    /// <summary>
    /// Represents a handler that executes actions after an event occured.
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface INotificationHandler<in TNotification> where TNotification : INotification
    {
        /// <summary>
        /// Executes actions in response to a notification.
        /// </summary>
        /// <param name="notification">Notification instance.</param>
        void Handle(TNotification notification);
    }
}
