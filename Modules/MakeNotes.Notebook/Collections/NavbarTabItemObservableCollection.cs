﻿using System;
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
            CollectionChanged += OnCollectionChanged;
        }
        
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PreserveAtLeastOneItem(e);
        }

        private void PreserveAtLeastOneItem(NotifyCollectionChangedEventArgs e)
        {
            // Do nothing if it's not a delete action and there are still items in the collection
            if (!_removeItemActions.Contains(e.Action) || Items.Any())
            {
                return;
            }
            
            Items.Add(_createDefaultItemFactory());
        }
    }
}
