using MakeNotes.Common.Core.Notifications;
using Prism.Events;

namespace MakeNotes.Framework.Events
{
    /// <summary>
    /// Generic event for communication between viewmodels that uses <see cref="INotification"/> as a parameter
    /// eliminating the need of creating separate <see cref="PubSubEvent{T}"/>> class for every kind of an event.
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public class ApplicationEvent<TNotification> : PubSubEvent<TNotification> where TNotification : INotification
    {
    }
}
