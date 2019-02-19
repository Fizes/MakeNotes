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
        private bool _isInitialized;
        private ObservableCollection<TVisualBlock> _items = new ObservableCollection<TVisualBlock>();

        public VisualBlockViewModelBase()
        {
            // int? instead of just int since Prism throws an exception if type is not nullable
            InitializeCommand = new DelegateCommand<int?>(id => InitilizeCore(id.GetValueOrDefault()));
        }

        protected int TabContentId { get; private set; }

        public ObservableCollection<TVisualBlock> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        
        public ICommand InitializeCommand { get; }

        private void InitilizeCore(int tabContentId)
        {
            if (!_isInitialized)
            {
                TabContentId = tabContentId;
                _isInitialized = true;
                Initialize(tabContentId);
            }
        }

        protected abstract void Initialize(int tabContentId);
    }
}
