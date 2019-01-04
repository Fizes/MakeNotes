namespace MakeNotes.Common.Core.Notifications
{
    /// <summary>
    /// Determines a strategy of executing the given <see cref="INotificationHandler{T}"/>.
    /// </summary>
    public interface INotificationStrategy
    {
        /// <summary>
        /// Executes a handler associated with the specified notification using a specific strategy.
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification"></param>
        void Publish<TNotification>(TNotification notification) where TNotification : INotification;
    }
}
