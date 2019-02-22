using System.Windows;
using System.Windows.Media;

namespace MakeNotes.Framework.Extensions
{
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// Finds an element of the specified type that represents the parent of the element in visual tree.
        /// Return null if no parent found.
        /// </summary>
        /// <typeparam name="T">Parent type.</typeparam>
        /// <param name="dependencyObject">Element which is used to find its parent.</param>
        /// <returns></returns>
        public static T FindParent<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            DependencyObject parent = dependencyObject;
            T result = null;

            while (result == null && parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);
                result = parent as T;
            }

            return result;
        }
    }
}
