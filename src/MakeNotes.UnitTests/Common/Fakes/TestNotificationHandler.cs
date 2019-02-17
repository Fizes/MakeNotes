using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.UnitTests.Common.Fakes
{
    public class Test1Notification : INotification
    {
    }

    public class Test2Notification : INotification
    {
    }

    public class Test1NotificationHandler : INotificationHandler<Test1Notification>
    {
        public void Handle(Test1Notification notification)
        {
        }
    }

    public class Test2NotificationHandler : INotificationHandler<Test2Notification>
    {
        public void Handle(Test2Notification notification)
        {
        }
    }
}
