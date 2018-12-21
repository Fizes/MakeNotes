using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using MakeNotes.Common.Core;
using MakeNotes.Common.Interfaces;
using MakeNotes.Framework.Factories;
using MakeNotes.Framework.Utilities;
using MakeNotes.Infrastructure;
using MakeNotes.Infrastructure.Extensions;
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
        private readonly IConfigurationRoot _configuration;

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

            var currentAssembly = Assembly.GetExecutingAssembly();
            var assemblies = currentAssembly
                .GetReferencedAssemblies()
                .Select(an => Assembly.Load(an))
                .ToArray();

            builder.RegisterAssemblyTypes(currentAssembly).PublicOnly();
            
            builder.RegisterAssemblyModules(assemblies);

            builder.RegisterInstance(_configuration.GetConfiguration<WindowSettings>());

            builder.RegisterType<ApplicationState>().As<IApplicationState>().SingleInstance();

            builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();
        }

        protected override void InitializeModules()
        {
            // NOTE: Method from the base class is not called to avoid NotSupportedException

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ToolbarRegion", typeof(Views.ToolbarView));
            regionManager.RegisterViewWithRegion("NavigationRegion", typeof(Notebook.Views.NavbarView));
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(Notebook.Views.TabContentView));
        }
    }
}
