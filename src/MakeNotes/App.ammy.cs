using System;
using System.Windows;
using AmmySidekick;
using MakeNotes.Framework.Extensions;

namespace MakeNotes
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var bootstrapper = new Bootstrapper();
            var resourcePath = $"/{Ammy.GetAssemblyName(bootstrapper)};component/App.g.xaml";

            bootstrapper.InitializeComponent(resourcePath);
#if DEBUG
            RuntimeUpdateHandler.Register(bootstrapper, resourcePath);
#endif

            bootstrapper.Run();
        }
    }
}
