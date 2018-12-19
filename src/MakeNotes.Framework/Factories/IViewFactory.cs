using System.Windows.Controls;

namespace MakeNotes.Framework.Factories
{
    /// <summary>
    /// View factory.
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// Creates a view of the specified type.
        /// </summary>
        /// <typeparam name="TView">View type.</typeparam>
        /// <returns></returns>
        TView Create<TView>() where TView : UserControl;
    }
}
