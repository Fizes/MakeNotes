using MakeNotes.Framework.Controls;
using MakeNotes.Infrastructure;

namespace MakeNotes
{
    public partial class MainWindow
    {
        public MainWindow(WindowSettings windowSettings, MainWindowViewModel viewModel)
        {
            WindowInitializer.AttachSystemCommands(this);

            InitializeComponent();

            Width = windowSettings.Width;
            Height = windowSettings.Height;
            WindowStartupLocation = windowSettings.StartupLocation;

            DataContext = viewModel;
        }
    }
}
