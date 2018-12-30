using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using MakeNotes.Common.Core;
using MakeNotes.Common.Core.Commands;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Core.Queries;
using MakeNotes.Common.Interfaces;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Infrastructure;
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

            var connectionString = SQLiteConnectionStringParser.Parse(_configuration.GetConnectionString("DefaultConnection"));
            builder.RegisterInstance(new DefaultConnectionFactory(connectionString)).As<IDbConnectionFactory>().SingleInstance();

            builder.Register(ctx => ctx.Resolve<IDbConnectionFactory>().Create()).As<IDbConnection>();

            builder.RegisterType<DapperRepository>().As<IRepository>();

            builder.RegisterInstance(_configuration.GetConfiguration<WindowSettings>());

            builder.RegisterType<ApplicationState>().As<IApplicationState>().SingleInstance();

            builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();

            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>));
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IQueryHandler<,>));
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(INotificationHandler<>));
            }

            builder.RegisterType<AutofacHandlerFactory>().As<IHandlerFactory>().SingleInstance();

            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().SingleInstance();

            builder.RegisterType<DefaultMessageBus>().As<IMessageBus>().SingleInstance();
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
