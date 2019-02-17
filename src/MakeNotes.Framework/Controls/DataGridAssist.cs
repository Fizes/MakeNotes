using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MakeNotes.Framework.Extensions;
using MouseWheelWeakEventManager = System.Windows.WeakEventManager<System.Windows.Controls.DataGrid, System.Windows.Input.MouseWheelEventArgs>;

namespace MakeNotes.Framework.Controls
{
    /// <summary>
    /// Attached properties for DataGrid.
    /// </summary>
    public static class DataGridAssist
    {
        private static Dictionary<int, ScrollViewer> _scrollViewers = new Dictionary<int, ScrollViewer>();

        public static readonly DependencyProperty DisableScrollBarProperty =
            DependencyProperty.RegisterAttached(
                "DisableScrollBar",
                typeof(bool),
                typeof(DataGridAssist),
                new PropertyMetadata(default(bool), OnDisableScrollBarPropertyChanged));

        public static bool GetDisableScrollBar(DependencyObject element)
        {
            return (bool)element.GetValue(DisableScrollBarProperty);
        }

        public static void SetDisableScrollBar(DependencyObject element, bool value)
        {
            element.SetValue(DisableScrollBarProperty, value);
        }

        private static void OnDisableScrollBarPropertyChanged(DependencyObject element, DependencyPropertyChangedEventArgs eventArgs)
        {
            var dataGrid = element as DataGrid;
            if (dataGrid == null)
            {
                return;
            }

            if ((bool)eventArgs.NewValue)
            {
                SetScrollBarVisibility(dataGrid, ScrollBarVisibility.Disabled);
                MouseWheelWeakEventManager.AddHandler(dataGrid, nameof(dataGrid.PreviewMouseWheel), DataGridOnPreviewMouseWheel);
            }
            else
            {
                SetScrollBarVisibility(dataGrid, ScrollBarVisibility.Auto);
                _scrollViewers.Remove(dataGrid.GetHashCode());
                MouseWheelWeakEventManager.RemoveHandler(dataGrid, nameof(dataGrid.PreviewMouseWheel), DataGridOnPreviewMouseWheel);
            }
        }

        private static void DataGridOnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            var scrollViewer = GetScrollViewer(dataGrid);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta / 2.5);
        }

        private static ScrollViewer GetScrollViewer(DataGrid dataGrid)
        {
            var dataGridKey = dataGrid.GetHashCode();
            if (!_scrollViewers.ContainsKey(dataGridKey))
            {
                var scrollViewer = dataGrid.FindParent<ScrollViewer>();
                _scrollViewers.Add(dataGridKey, scrollViewer);
            }

            return _scrollViewers[dataGridKey];
        }

        private static void SetScrollBarVisibility(DataGrid dataGrid, ScrollBarVisibility visibility)
        {
            dataGrid.HorizontalScrollBarVisibility = visibility;
            dataGrid.VerticalScrollBarVisibility = visibility;
        }
    }
}
