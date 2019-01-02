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
            _eventAggregator.GetEvent<TabSelectedEvent>().Subscribe(OnTabSelected);
        }

        public int CurrentTabId => _applicationState.GetValue<int>(nameof(CurrentTabId));

        private void OnTabSelected(int tabId)
        {
            _applicationState.SetValue(nameof(CurrentTabId), tabId);
        }
    }
}
