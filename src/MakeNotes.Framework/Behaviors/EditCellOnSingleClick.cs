using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using MakeNotes.Framework.Extensions;

namespace MakeNotes.Framework.Behaviors
{
    public class EditCellOnSingleClick : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            AssociatedObject.LoadingRow += OnLoadingRow;
            AssociatedObject.UnloadingRow += OnUnloadingRow;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.LoadingRow -= OnLoadingRow;
            AssociatedObject.UnloadingRow -= OnUnloadingRow;
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private void OnUnloadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cell = ((DependencyObject)e.OriginalSource).FindParent<DataGridCell>();
            if (cell == null || cell.IsEditing || cell.IsReadOnly)
            {
                return;
            }

            if (cell.Focusable && !cell.IsFocused)
            {
                cell.Focus();
                AssociatedObject.BeginEdit();
            }
        }
    }
}
