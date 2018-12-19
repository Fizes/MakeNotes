using System.Collections.ObjectModel;
using MakeNotes.Notebook.Models;
using Prism.Mvvm;

namespace MakeNotes.Notebook.Views.Templates
{
    public class PasswordDataGridTemplateViewModel : BindableBase
    {
        public ObservableCollection<PasswordDataGridItem> Items { get; } = new ObservableCollection<PasswordDataGridItem>();
    }
}
