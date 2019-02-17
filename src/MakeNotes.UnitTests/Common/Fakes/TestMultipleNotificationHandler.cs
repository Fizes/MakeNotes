using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.UnitTests.Common.Fakes
{
    public class Test1MultipleNotification : INotification
    {
    }

    public class Test2MultipleNotification : INotification
    {
    }

    public class TestMultipleNotificationHandler : INotificationHandler<Test1MultipleNotification>,
                                                   INotificationHandler<Test2MultipleNotification>
    {
        public void Handle(Test1MultipleNotification notification)
        {
        }

        public void Handle(Test2MultipleNotification notification)
        {
        }
    }
}
