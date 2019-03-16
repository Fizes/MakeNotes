using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.
        /// </summary>
        /// <typeparam name="T">Type of elements in the collection.</typeparam>
        /// <param name="source">The collection into which the items are added.</param>
        /// <param name="items">The collection whose elements should be added to the end of
        /// the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</param>
        public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                source.Add(item);
            }
        }
    }
}
