using MakeNotes.Framework.Controls;
using MakeNotes.Infrastructure;

namespace MakeNotes
{
    public partial class MainWindow
    {
        public MainWindow(WindowSettings windowSettings)
        {
            WindowInitializer.AttachSystemCommands(this);

            InitializeComponent();

            Width = windowSettings.Width;
            Height = windowSettings.Height;
            WindowStartupLocation = windowSettings.StartupLocation;
        }
    }
}
