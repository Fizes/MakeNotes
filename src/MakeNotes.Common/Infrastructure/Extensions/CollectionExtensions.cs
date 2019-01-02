using System.Collections.Generic;
using System.Linq;

namespace MakeNotes.Common.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns an element that is before the specified element in the list.
        /// If the element is first element in the list next element is returned.
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="source">List to search for the element.</param>
        /// <param name="element">Element that comes after.</param>
        /// <returns></returns>
        public static T GetPreviousElement<T>(this IList<T> source, T element)
        {
            var index = source.IndexOf(element);

            var previousIndex = index - 1;
            if (previousIndex > -1)
            {
                return source[previousIndex];
            }

            var nextIndex = index + 1;
            if (nextIndex < source.Count())
            {
                return source[nextIndex];
            }

            return default;
        }
    }
}
