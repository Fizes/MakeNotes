using System.Windows;
using Autofac;
using MakeNotes.Framework.Controls;

namespace MakeNotes
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            WindowInitializer.AttachSystemCommands(this);

            InitializeComponent();

#if DEBUG
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
#endif

            DataContext = App.DependencyResolver.Resolve<MainWindowViewModel>();
        }
    }
}
