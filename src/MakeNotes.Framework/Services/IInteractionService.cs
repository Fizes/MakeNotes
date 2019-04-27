using System.Threading.Tasks;
using System.Windows.Controls;
using MakeNotes.Framework.Models;
using MaterialDesignThemes.Wpf;

namespace MakeNotes.Framework.Services
{
    /// <summary>
    /// Serves to interact with a user through modal dialogs.
    /// </summary>
    public interface IInteractionService
    {
        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <returns></returns>
        Task ShowAsync<TView>() where TView : UserControl, new();

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <param name="viewModel">DataContext of the view.</param>
        /// <returns></returns>
        Task ShowAsync<TView>(object viewModel) where TView : UserControl, new();

        /// <summary>
        /// Shows a modal dialog with content of the specified view.
        /// </summary>
        /// <typeparam name="TView">Type of view used as content.</typeparam>
        /// <param name="viewModel">DataContext of the view.</param>
        /// <param name="closedEventHandler">A handler that is called when a dialog is closed passing
        /// value of the parameter given to <see cref="DialogHost.CloseDialogCommand"/>.</param>
        /// <returns></returns>
        Task ShowAsync<TView>(object viewModel, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new();

        /// <summary>
        /// Shows a modal confirmation dialog with the specified title and text.
        /// </summary>
        /// <param name="closedEventHandler">A handler that is called when a dialog is closed.</param>
        /// <returns></returns>
        Task ShowConfirmationAsync(string title, string text, DialogClosedEventHandler closedEventHandler);
    }

    /// <summary>
    /// Signature of a closed dialog handler.
    /// </summary>
    /// <param name="result"></param>
    public delegate void DialogClosedEventHandler(DialogResult result);
}
