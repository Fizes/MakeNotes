using System.Collections.Concurrent;
using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Used to store events occured in the system to be processed afterwards.
    /// </summary>
    public static class ApplicationEvents
    {
        internal static ConcurrentQueue<INotification> Notifications { get; } = new ConcurrentQueue<INotification>();

        /// <summary>
        /// Appends a notification (event) to the queue.
        /// </summary>
        /// <typeparam name="TNotification">Notification type.</typeparam>
        /// <param name="notification">Notification instance with parameters.</param>
        public static void Raise<TNotification>(TNotification notification) where TNotification : INotification
        {
            Notifications.Enqueue(notification);
        }
    }
}
