using MakeNotes.Common.Core.Notifications;
using MakeNotes.Framework.Events;
using Prism.Events;

namespace MakeNotes.Notebook.Core.Notifications.Handlers
{
    public class TabNotificationHandler : INotificationHandler<TabDeleted>,
                                          INotificationHandler<TabSelected>
    {
        private readonly IEventAggregator _eventAggregator;

        public TabNotificationHandler(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        
        public void Handle(TabDeleted notification)
        {
        }

        public void Handle(TabSelected notification)
        {
            _eventAggregator.GetEvent<ApplicationEvent<TabSelected>>().Publish(notification);
        }
    }
}
