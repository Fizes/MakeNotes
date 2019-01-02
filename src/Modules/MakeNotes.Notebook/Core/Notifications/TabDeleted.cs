using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Notebook.Core.Notifications
{
    public class TabDeleted : INotification
    {
        public TabDeleted(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
