using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Windows.Controls;
using MakeNotes.Framework.Models;
using MaterialDesignThemes.Wpf;

namespace MakeNotes.Framework.Controls
{
    /// <summary>
    /// Manages modal dialogs.
    /// </summary>
    public static class DialogManager
    {
        private const string RootDialogIdentifier = "RootDialogArea";

        public delegate void DialogClosedEventHandler(DialogResult result);

        // Calls the appropriate delegate depending on value of the parameter sent along with the dialog close command
        private static void OnCloseDialog(DialogClosingEventArgs e, Action onSuccess, Action onCancel, DialogClosedEventHandler closedEventHandler)
        {
            var parameter = e.Parameter?.ToString();
            var dialogResult = DialogResult.Unspecified;

            // Try to parse against enum values, otherwise try to parse against bool
            if (!Enum.TryParse(parameter, out DialogResult parsedEnumValue))
            {
                if (bool.TryParse(parameter, out bool parsedBoolValue))
                {
                    dialogResult = (DialogResult)Convert.ToInt32(parsedBoolValue);
                }
            }
            
            switch (dialogResult)
            {
                case DialogResult.Canceled:
                    onCancel?.Invoke();
                    break;
                case DialogResult.Accepted:
                    onSuccess?.Invoke();
                    break;
            }

            closedEventHandler?.Invoke(dialogResult);
        }

        // Caches a view to reuse it without performance hit
        private static TView GetOrCreateView<TView>() where TView : UserControl, new()
        {
            var cache = MemoryCache.Default;
            var key = typeof(TView).FullName;
            var view = cache.Get(key) as TView;

            if (view == null)
            {
                view = Activator.CreateInstance<TView>();
                cache.Add(key, view, absoluteExpiration: DateTime.MaxValue);
            }

            return view;
        }

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <returns></returns>
        public async static Task Show<TView>() where TView : UserControl, new()
        {
            await Show<TView>(null, null, null, null);
        }

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <param name="viewModel">DataContext of the view.</param>
        /// <param name="closedEventHandler">A handler that is called when dialog is closed returning value of the parameter given to <see cref="DialogHost.CloseDialogCommand"/>.</param>
        /// <returns></returns>
        public async static Task Show<TView>(object viewModel, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new()
        {
            await Show<TView>(viewModel, null, null, closedEventHandler);
        }

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <param name="viewModel">DataContext of the view.</param>
        /// <param name="onSuccess">A handler that is called if <see cref="DialogHost.CloseDialogCommand"/> was invoked with a parameter equal to <see cref="true"/>.</param>
        /// <returns></returns>
        public async static Task Show<TView>(object viewModel, Action onSuccess) where TView : UserControl, new()
        {
            await Show<TView>(viewModel, onSuccess, null, null);
        }

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <param name="viewModel">DataContext of the view.</param>
        /// <param name="onSuccess">A handler that is called if <see cref="DialogHost.CloseDialogCommand"/> was invoked with a parameter equal to <see cref="true"/>.</param>
        /// <param name="closedEventHandler">A handler that is called when dialog is closed returning value of the parameter given to <see cref="DialogHost.CloseDialogCommand"/>.</param>
        /// <returns></returns>
        public async static Task Show<TView>(object viewModel, Action onSuccess, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new()
        {
            await Show<TView>(viewModel, onSuccess, null, closedEventHandler);
        }

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <param name="viewModel">DataContext of the view.</param>
        /// <param name="onSuccess">A handler that is called if <see cref="DialogHost.CloseDialogCommand"/> was invoked with a parameter equal to <see cref="true"/>.</param>
        /// <param name="onCancel">A handler that is called if <see cref="DialogHost.CloseDialogCommand"/> was invoked with a parameter equal to <see cref="false"/>.</param>
        /// <param name="closedEventHandler">A handler that is called when dialog is closed returning value of the parameter given to <see cref="DialogHost.CloseDialogCommand"/>.</param>
        /// <returns></returns>
        public async static Task Show<TView>(object viewModel, Action onSuccess, Action onCancel, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new()
        {
            var view = GetOrCreateView<TView>();
            view.DataContext = viewModel;
            await DialogHost.Show(view, RootDialogIdentifier, (s, e) => OnCloseDialog(e, onSuccess, onCancel, closedEventHandler));
        }
    }
}
