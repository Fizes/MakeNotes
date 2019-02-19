using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Notebook.Core.Notifications
{
    public class TabContentDeleted : INotification
    {
        public TabContentDeleted(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}