using System.Collections.ObjectModel;
using System.Windows.Input;
using MakeNotes.Common.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace MakeNotes.Notebook.Templates.VisualBlocks.ViewModels
{
    public abstract class VisualBlockViewModelBase<TVisualBlock> : BindableBase, IVisualBlockViewModel
        where TVisualBlock : IVisualBlock
    {
        private ObservableCollection<TVisualBlock> _items = new ObservableCollection<TVisualBlock>();

        public VisualBlockViewModelBase()
        {
            // int? instead of just int since Prism throws an exception if type is not nullable
            InitializeCommand = new DelegateCommand<int?>(id => Initialize(id.GetValueOrDefault()));
        }

        public ObservableCollection<TVisualBlock> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ICommand InitializeCommand { get; }

        protected abstract void Initialize(int tabContentId);
    }
}
