using System;
using MakeNotes.Notebook.Events;
using Prism.Events;

namespace MakeNotes.Notebook.Providers
{
    public class NavigationContext : INavigationContext
    {
        private readonly IEventAggregator _eventAggregator;

        public NavigationContext(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<TabChangedEvent>().Subscribe(OnTabChanged);
        }

        public Guid CurrentTabId { get; private set; }

        private void OnTabChanged(Guid tabId)
        {
            CurrentTabId = tabId;
        }
    }
}
