using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MakeNotes.Framework.Attributes;
using MakeNotes.Framework.Extensions;
using MakeNotes.Framework.Validation;
using MouseWheelWeakEventManager = System.Windows.WeakEventManager<System.Windows.Controls.DataGrid, System.Windows.Input.MouseWheelEventArgs>;

namespace MakeNotes.Framework.Controls
{
    /// <summary>
    /// Attached properties for DataGrid.
    /// </summary>
    public static class DataGridAssist
    {
        private static readonly Dictionary<int, ScrollViewer> _scrollViewers = new Dictionary<int, ScrollViewer>();
        private static readonly Dictionary<DataType, DataGridColumnFactory> _dataGridColumnFactories = new Dictionary<DataType, DataGridColumnFactory>();

        private delegate DataGridColumn DataGridColumnFactory(DataGrid dataGrid, PropertyDescriptor descriptor);

        static DataGridAssist()
        {
            _dataGridColumnFactories.Add(DataType.Text, CreteDataGridTextColumn);
        }

        private static DataGridTextColumn CreteDataGridTextColumn(DataGrid dataGrid, PropertyDescriptor descriptor)
        {
            var binding = new Binding(descriptor.Name);
            var requiredAttr = descriptor.Attributes.Get<RequiredAttribute>();
            if (requiredAttr != null)
            {
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding.ValidationRules.Add(new NotEmptyValidationRule { ValidatesOnTargetUpdated = true });
            }

            var column = new DataGridTextColumn
            {
                EditingElementStyle = (Style)dataGrid.FindResource("DataGridTextColumnEditingStyle"),
                ElementStyle = (Style)dataGrid.FindResource("DataGridTextColumnElementStyle"),
                Binding = binding
            };

            return column;
        }

        #region DisableScrollBar
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
            if (!(element is DataGrid dataGrid))
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
        #endregion

        #region AutoGenerateColumnsFromMetadata
        public static readonly DependencyProperty AutoGenerateColumnsFromMetadataProperty =
            DependencyProperty.RegisterAttached(
                "AutoGenerateColumnsFromMetadata",
                typeof(bool),
                typeof(DataGridAssist),
                new PropertyMetadata(default(bool), OnAutoGenerateColumnsFromMetadataPropertyChanged));

        public static bool GetAutoGenerateColumnsFromMetadata(DependencyObject element)
        {
            return (bool)element.GetValue(AutoGenerateColumnsFromMetadataProperty);
        }

        public static void SetAutoGenerateColumnsFromMetadata(DependencyObject element, bool value)
        {
            element.SetValue(AutoGenerateColumnsFromMetadataProperty, value);
        }

        private static void OnAutoGenerateColumnsFromMetadataPropertyChanged(DependencyObject element, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(element is DataGrid dataGrid))
            {
                return;
            }

            var initialAutoGenerateColumnsValue = dataGrid.AutoGenerateColumns;
            if ((bool)eventArgs.NewValue)
            {
                dataGrid.AutoGenerateColumns = false;
                dataGrid.Loaded += DataGridOnLoaded;
            }
            else
            {
                dataGrid.AutoGenerateColumns = initialAutoGenerateColumnsValue;
                dataGrid.Loaded -= DataGridOnLoaded;
            }
        }

        private static void DataGridOnLoaded(object sender, RoutedEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            dataGrid.Loaded -= DataGridOnLoaded;
            var itemProperties = ((IItemProperties)dataGrid.Items).ItemProperties;

            if (itemProperties != null && itemProperties.Count > 0)
            {
                GenerateColumns(itemProperties, dataGrid);
            }
        }

        private static void GenerateColumns(ReadOnlyCollection<ItemPropertyInfo> itemProperties, DataGrid dataGrid)
        {
            var newColumns = new List<DataGridColumn>(itemProperties.Count + dataGrid.Columns.Count);

            foreach (var itemProperty in itemProperties)
            {
                var descriptor = (PropertyDescriptor)itemProperty.Descriptor;

                var ignoreAttr = descriptor.Attributes.Get<IgnoreAttribute>();
                if (ignoreAttr != null)
                {
                    continue;
                }

                var dataGridColumn = CreateDataGridColumn(dataGrid, descriptor);

                var displayAttr = descriptor.Attributes.Get<DisplayAttribute>();
                if (displayAttr != null)
                {
                    dataGridColumn.Header = displayAttr.Name;
                }
                else
                {
                    dataGridColumn.Header = itemProperty.Name;
                }

                newColumns.Add(dataGridColumn);
            }

            PrependColumns(dataGrid, newColumns);
        }

        private static void PrependColumns(DataGrid dataGrid, List<DataGridColumn> prependingColumns)
        {
            prependingColumns.AddRange(dataGrid.Columns);
            dataGrid.Columns.Clear();
            foreach (var column in prependingColumns)
            {
                dataGrid.Columns.Add(column);
            }
        }

        private static DataGridColumn CreateDataGridColumn(DataGrid dataGrid, PropertyDescriptor descriptor)
        {
            var dataTypeAttr = descriptor.Attributes.Get<DataTypeAttribute>();
            if (dataTypeAttr != null && _dataGridColumnFactories.TryGetValue(dataTypeAttr.DataType, out DataGridColumnFactory createColumnFactory))
            {
                return createColumnFactory(dataGrid, descriptor);
            }

            return CreteDataGridTextColumn(dataGrid, descriptor);
        }
        #endregion
    }
}
