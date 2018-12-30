using System.Collections.Generic;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Factory for creating query, command and notification handlers.
    /// </summary>
    public interface IHandlerFactory
    {
        /// <summary>
        /// Creates an instance of a handler of the specified type.
        /// </summary>
        /// <typeparam name="THandler">Handler type.</typeparam>
        /// <returns></returns>
        THandler Create<THandler>() where THandler : IHandler;

        /// <summary>
        /// Creates a list of handler instances of the specified notification type.
        /// </summary>
        /// <typeparam name="TNotificationHandler">Notification handler type.</typeparam>
        /// <returns></returns>
        IEnumerable<TNotificationHandler> CreateAll<TNotificationHandler>() where TNotificationHandler : IHandler;
    }
}
