using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Notebook.Core.Notifications
{
    public class VisualBlockDeleted : INotification
    {
        public VisualBlockDeleted(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
