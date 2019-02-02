using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using MakeNotes.Common.Models;
using MakeNotes.Notebook.Consts;

namespace MakeNotes.Notebook.Collections
{
    /// <summary>
    /// <see cref="NavbarTabItem"/> collection implementation.
    /// </summary>
    public class NavbarTabItemObservableCollection : ObservableCollection<NavbarTabItem>
    {
        private static readonly NotifyCollectionChangedAction[] _removeItemActions = new[]
        {
            NotifyCollectionChangedAction.Remove,
            NotifyCollectionChangedAction.Reset
        };

        private static readonly Func<NavbarTabItem> _createDefaultItemFactory = () => new NavbarTabItem(DefaultValues.DefaultTabName, 0);

        public NavbarTabItemObservableCollection()
        {
            Add(_createDefaultItemFactory());
        }

        public NavbarTabItemObservableCollection(IEnumerable<NavbarTabItem> collection) : base(collection)
        {
            if (!collection.Any())
            {
                Add(_createDefaultItemFactory());
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            PreserveAtLeastOneItem(e);
        }
        
        private void PreserveAtLeastOneItem(NotifyCollectionChangedEventArgs e)
        {
            // When a collection is being cleared
            if (_removeItemActions.Contains(e.Action) && !Items.Any())
            {
                Add(_createDefaultItemFactory());
            }
        }
    }
}
