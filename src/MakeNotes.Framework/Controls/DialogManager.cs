using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Windows.Controls;
using MakeNotes.Framework.Models;
using MaterialDesignThemes.Wpf;

namespace MakeNotes.Framework.Controls
{
    /// <summary>
    /// Provides methods for working with modal dialogs.
    /// </summary>
    public static class DialogManager
    {
        private const string RootDialogIdentifier = "RootDialogArea";

        public delegate void DialogClosedEventHandler(DialogResult result);

        // Makes an attempt to parse the value either as DialogResult or bool
        private static DialogResult ParseDialogResult(string value)
        {
            if (bool.TryParse(value, out bool parsedBoolValue))
            {
                return (DialogResult)Convert.ToInt32(parsedBoolValue);
            }

            if (Enum.TryParse(value, out DialogResult parsedEnumValue))
            {
                return parsedEnumValue;
            }
            
            return DialogResult.Unspecified;
        }

        // Parses the event parameter and invokes the handler with passing the parsed parameter to it
        private static void OnCloseDialog(DialogClosingEventArgs e, DialogClosedEventHandler closedEventHandler)
        {
            var parameter = e.Parameter?.ToString();
            var dialogResult = ParseDialogResult(parameter);
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
            await Show<TView>(null, null);
        }

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <param name="viewModel">DataContext of the view.</param>
        /// <returns></returns>
        public async static Task Show<TView>(object viewModel) where TView : UserControl, new()
        {
            await Show<TView>(viewModel, null);
        }

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <param name="viewModel">DataContext of the view.</param>
        /// <param name="closedEventHandler">A handler that is called when a dialog is closed passing
        /// value of the parameter given to <see cref="DialogHost.CloseDialogCommand"/>.</param>
        /// <returns></returns>
        public async static Task Show<TView>(object viewModel, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new()
        {
            var view = GetOrCreateView<TView>();
            view.DataContext = viewModel;
            await DialogHost.Show(view, RootDialogIdentifier, (s, e) => OnCloseDialog(e, closedEventHandler));
        }
    }
}
