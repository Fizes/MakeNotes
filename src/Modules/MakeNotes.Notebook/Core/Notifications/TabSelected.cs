using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Notebook.Core.Notifications
{
    public class TabSelected : INotification
    {
        public TabSelected(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
