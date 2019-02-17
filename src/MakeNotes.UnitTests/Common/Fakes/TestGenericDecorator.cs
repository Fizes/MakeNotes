using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.UnitTests.Common.Fakes
{
    public class TestGenericDecorator<T> : INotificationHandler<T> where T : INotification
    {
        public TestGenericDecorator(INotificationHandler<T> decorated)
        {
        }

        public void Handle(T notification)
        {
        }
    }
}
