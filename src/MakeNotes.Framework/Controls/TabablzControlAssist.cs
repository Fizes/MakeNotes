using System.Windows;
using System.Windows.Input;

namespace MakeNotes.Framework.Controls
{
    /// <summary>
    /// Attached properties to extend TabablzControl.
    /// </summary>
    public static class TabablzControlAssist
    {
        public static readonly DependencyProperty CloseTabCommandProperty =
            DependencyProperty.RegisterAttached(
                "CloseTabCommand",
                typeof(ICommand),
                typeof(TabablzControlAssist),
                new FrameworkPropertyMetadata(default(ICommand), FrameworkPropertyMetadataOptions.Inherits));

        public static ICommand GetCloseTabCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(CloseTabCommandProperty);
        }
        
        public static void SetCloseTabCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CloseTabCommandProperty, value);
        }
    }
}