using System;
using System.Windows;
using AmmySidekick;
using Autofac;
using MakeNotes.Framework.Utilities;
using MakeNotes.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace MakeNotes
{
    public partial class App : Application
    {
        private static IContainer _container;

        [STAThread]
        public static void Main()
        {
            Configure();

            CreateApp().Run();
        }

        internal static IComponentContext DependencyResolver => _container;

        private static App CreateApp()
        {
            App app = new App();
            app.InitializeComponent();
            app.Exit += (s, e) => _container?.Dispose();

            RuntimeUpdateHandler.Register(app, $"/{Ammy.GetAssemblyName(app)};component/App.g.xaml");

            return app;
        }

        private static void Configure()
        {
            var environment = EnvironmentUtility.GetCommandLineVariable("APP_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

            var locale = configuration.GetValue<string>("AppLocale");
            EnvironmentUtility.SetCurrentLocale(locale);

            _container = AutofacConfig.Configure();
        }
    }
}
