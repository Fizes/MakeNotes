using System.Windows;
using Autofac;
using Dapper.AmbientContext;
using MakeNotes.DAL.Infrastructure;
using MakeNotes.Framework.Utilities;
using MakeNotes.Infrastructure;
using Microsoft.Extensions.Configuration;
using Prism.Autofac;
using Prism.Ioc;
using Prism.Regions;

namespace MakeNotes
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    public class Bootstrapper : PrismApplication
    {
        private readonly IConfiguration _configuration;

        public Bootstrapper()
        {
            var environment = EnvironmentUtility.GetCommandLineVariable("APP_ENVIRONMENT");
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

            var locale = _configuration.GetValue<string>("AppLocale");
            EnvironmentUtility.SetCurrentLocale(locale);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var builder = containerRegistry.GetBuilder();
            AutofacConfig.Configure(builder, _configuration);
        }

        protected override void InitializeModules()
        {
            // NOTE: Method from the base class is not called to avoid NotSupportedException

            var dbConnectionFactory = Container.Resolve<IDbConnectionFactory>();
            DatabaseMigrator.Migrate(dbConnectionFactory);

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ToolbarRegion", typeof(Views.ToolbarView));
            regionManager.RegisterViewWithRegion("NavigationRegion", typeof(Notebook.Views.NavbarView));
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(Notebook.Views.TabContentView));
        }
    }
}
