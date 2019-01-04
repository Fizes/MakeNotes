using MakeNotes.Common.Interfaces;
using MakeNotes.Framework.Events;
using MakeNotes.Notebook.Core.Notifications;
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

            _eventAggregator.GetEvent<ApplicationEvent<TabSelected>>().Subscribe(OnTabSelected);
        }

        public int CurrentTabId => _applicationState.GetValue<int>(nameof(CurrentTabId));

        #region Methods
        private void OnTabSelected(TabSelected notification)
        {
            _applicationState.SetValue(nameof(CurrentTabId), notification.Id);
        }
        #endregion
    }
}
