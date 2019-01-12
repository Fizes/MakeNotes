using System.Collections.ObjectModel;

namespace MakeNotes.Notebook.Models
{
    public class TabContent
    {
        public int Id { get; set; }

        public int TabId { get; set; }

        public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();
    }
}
