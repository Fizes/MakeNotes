using System.Linq;
using System.Reflection;
using Autofac;
using Dapper.AmbientContext;
using Dapper.AmbientContext.Storage;
using MakeNotes.Common.Core;
using MakeNotes.Common.Core.Factories;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Core.Requests;
using MakeNotes.Common.Infrastructure;
using MakeNotes.Common.Interfaces;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Infrastructure;
using MakeNotes.Framework.Services;
using MakeNotes.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;

namespace MakeNotes.Infrastructure
{
    public static class AutofacConfig
    {
        /// <summary>
        /// Configures application dependencies.
        /// </summary>
        /// <param name="builder">Autofac builder.</param>
        /// <param name="configuration">Application configuration.</param>
        public static ContainerBuilder Configure(ContainerBuilder builder, IConfiguration configuration)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var assemblies = currentAssembly
                .GetReferencedAssemblies()
                .Select(an => Assembly.Load(an))
                .ToArray();

            builder.RegisterAssemblyTypes(currentAssembly).PublicOnly();
            
            AmbientDbContextStorageProvider.SetStorage(new LogicalCallContextStorage());
            var connectionString = SQLiteConnectionStringParser.Parse(configuration.GetConnectionString("DefaultConnection"));
            builder.RegisterInstance(new DefaultConnectionFactory(connectionString)).As<IDbConnectionFactory>().SingleInstance();
            builder.RegisterType<AmbientDbContextFactory>().As<IAmbientDbContextFactory>().SingleInstance();
            builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>();

            builder.RegisterType<DapperRepository>().As<IRepository>();

            builder.RegisterInstance(configuration.GetConfiguration<WindowSettings>());

            builder.RegisterType<ApplicationState>().As<IApplicationState>().SingleInstance();

            builder.RegisterType<InteractionService>().As<IInteractionService>().SingleInstance();

            builder.RegisterType<BackgroundTask>().As<IBackgroundTask>().SingleInstance();

            builder.RegisterType<AutofacHandlerFactory>().As<IHandlerFactory>().SingleInstance();
            builder.RegisterType<DefaultMessageBus>().As<IMessageBus>().SingleInstance();

            builder.RegisterType<DefaultNotificationStrategy>().As<INotificationStrategy>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<FireAndForgetNotificationStrategy>().As<INotificationStrategy>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AutofacNotificationStrategyFactory>().As<INotificationStrategyFactory>().SingleInstance();

            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => !t.Name.EndsWith("Decorator"))
                    .AsClosedTypesOf(typeof(INotificationHandler<>))
                    .InstancePerLifetimeScope();
            }

            builder.RegisterAssemblyModules(assemblies);

            return builder;
        }
    }
}
