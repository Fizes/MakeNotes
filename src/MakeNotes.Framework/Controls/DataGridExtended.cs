using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using MakeNotes.Framework.Behaviors;

namespace MakeNotes.Framework.Controls
{
    public class DataGridExtended : DataGrid
    {
        private bool _changesCommitted;
        private DataGridRow _lastEditedRow;

        public event EventHandler CellEditEnded;

        public DataGridExtended()
        {
            LoadingRow += OnLoadingRow;
            UnloadingRow += OnUnloadingRow;

            DataGridAssist.SetDisableScrollBar(this, true);
            DataGridAssist.SetAutoGenerateColumnsFromMetadata(this, true);

            var behaviors = Interaction.GetBehaviors(this);
            behaviors.Add(new EditCellOnSingleClick());
        }

        private static readonly DependencyPropertyKey EditedItemPropertyKey =
            DependencyProperty.RegisterReadOnly("EditedItem",
                typeof(object),
                typeof(DataGridExtended),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty EditedItemProperty = EditedItemPropertyKey.DependencyProperty;
        
        /// <summary>
        /// Last edited item.
        /// </summary>
        public object EditedItem
        {
            get { return GetValue(EditedItemProperty); }
            protected set { SetValue(EditedItemPropertyKey, value); }
        }
        
        protected override void OnBeginningEdit(DataGridBeginningEditEventArgs e)
        {
            base.OnBeginningEdit(e);
            _changesCommitted = e.Row != _lastEditedRow;
        }

        protected override void OnCellEditEnding(DataGridCellEditEndingEventArgs e)
        {
            _lastEditedRow = e.Row;
            base.OnCellEditEnding(e);
        }

        protected override void OnExecutedCommitEdit(ExecutedRoutedEventArgs e)
        {
            base.OnExecutedCommitEdit(e);
            EditedItem = _lastEditedRow?.Item;
            _changesCommitted = true;
        }

        /// <summary>
        /// Override to not block editing other cells when there is a validation error.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCanExecuteBeginEdit(CanExecuteRoutedEventArgs e)
        {
            if (IsReadOnly)
            {
                base.OnCanExecuteBeginEdit(e);
            }
            else
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.LostFocus += OnRowLostFocus;
        }

        private void OnUnloadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.LostFocus -= OnRowLostFocus;
        }

        private void OnRowLostFocus(object sender, RoutedEventArgs e)
        {
            if (_changesCommitted && EditedItem != null)
            {
                CellEditEnded?.Invoke(this, e);
                _changesCommitted = false;
            }
        }
    }
}
