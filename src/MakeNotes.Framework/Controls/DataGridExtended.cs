using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using MakeNotes.Framework.Behaviors;

namespace MakeNotes.Framework.Controls
{
    public class DataGridExtended : DataGrid
    {
        public DataGridExtended()
        {
            DataGridAssist.SetDisableScrollBar(this, true);
            DataGridAssist.SetAutoGenerateColumnsFromMetadata(this, true);

            var behaviors = Interaction.GetBehaviors(this);
            behaviors.Add(new EditCellOnSingleClick());
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
    }
}
