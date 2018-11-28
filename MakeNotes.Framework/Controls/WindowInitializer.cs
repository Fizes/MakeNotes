using System.Windows;
using System.Windows.Input;

namespace MakeNotes.Framework.Controls
{
    /// <summary>
    /// Contains methods for initializing a window.
    /// </summary>
    public static class WindowInitializer
    {
        /// <summary>
        /// Attaches system commands such as maximizing, minimizing, etc. to the window.
        /// </summary>
        /// <param name="window"></param>
        public static void AttachSystemCommands(Window window)
        {
            window.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (s, e) => OnCloseWindow(window)));

            window.CommandBindings.Add(
                new CommandBinding(SystemCommands.MaximizeWindowCommand, (s, e) => OnMaximizeWindow(window), (s, e) => OnCanResizeWindow(window, e)));

            window.CommandBindings.Add(
                new CommandBinding(SystemCommands.MinimizeWindowCommand, (s, e) => OnMinimizeWindow(window), (s, e) => OnCanMinimizeWindow(window, e)));

            window.CommandBindings.Add(
                new CommandBinding(SystemCommands.RestoreWindowCommand, (s, e) => OnRestoreWindow(window), (s, e) => OnCanResizeWindow(window, e)));

            window.Closed += (s, e) => Application.Current.Shutdown();
        }

        private static void OnCloseWindow(Window window)
        {
            SystemCommands.CloseWindow(window);
        }

        private static void OnMaximizeWindow(Window window)
        {
            SystemCommands.MaximizeWindow(window);
        }

        private static void OnMinimizeWindow(Window window)
        {
            SystemCommands.MinimizeWindow(window);
        }

        private static void OnRestoreWindow(Window window)
        {
            SystemCommands.RestoreWindow(window);
        }

        private static void OnCanResizeWindow(Window window, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = window.ResizeMode == ResizeMode.CanResize || window.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private static void OnCanMinimizeWindow(Window window, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = window.ResizeMode != ResizeMode.NoResize;
        }
    }
}
