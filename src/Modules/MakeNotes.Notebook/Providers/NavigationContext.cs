using System;
using MakeNotes.Common.Interfaces;
using MakeNotes.Notebook.Events;
using Prism.Events;

namespace MakeNotes.Notebook.Providers
{
    public class NavigationContext : INavigationContext
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplicationState _applicationState;

        public NavigationContext(IEventAggregator eventAggregator, IApplicationState applicationState)
        {
            _eventAggregator = eventAggregator;
            _applicationState = applicationState;
            _eventAggregator.GetEvent<TabChangedEvent>().Subscribe(OnTabChanged);
        }

        public Guid CurrentTabId => _applicationState.GetValue<Guid>(nameof(CurrentTabId));

        private void OnTabChanged(Guid tabId)
        {
            _applicationState.SetValue(nameof(CurrentTabId), tabId);
        }
    }
}
