using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using AmmySidekick;
using MakeNotes.Framework.Utilities;
using Microsoft.Extensions.Configuration;

namespace MakeNotes
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            Configure();

            CreateApp().Run();
        }

        private static App CreateApp()
        {
            App app = new App();
            app.InitializeComponent();

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
            SetLocale(locale);
        }

        private static void SetLocale(string locale)
        {
            if (String.IsNullOrWhiteSpace(locale))
            {
                return;
            }

            var culture = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var metadata = new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag));
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), metadata);
        }
    }
}
