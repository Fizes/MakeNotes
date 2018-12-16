using System;
using Prism.Events;

namespace MakeNotes.Notebook.Events
{
    /// <summary>
    /// Event that occurs when a tab is changed.
    /// </summary>
    public class TabChangedEvent : PubSubEvent<Guid>
    {
    }
}
