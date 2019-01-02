using System.Collections.ObjectModel;
using MakeNotes.Common.Models;

namespace MakeNotes.Notebook.Models
{
    public class TabContent
    {
        public int TabId { get; set; }
        
        public ObservableCollection<IDynamicElement> Items { get; } = new ObservableCollection<IDynamicElement>();
    }
}
