using System.Globalization;
using System.IO;
using System.Threading;
using Autofac;
using MakeNotes.Common.Infrastructure.Extensions;
using MakeNotes.DAL.Infrastructure;
using MakeNotes.Framework.Services;
using MakeNotes.Infrastructure;
using MakeNotes.IntegrationTests.Infrastructure.Fakes;
using Microsoft.Extensions.Configuration;
using Prism.Events;

namespace MakeNotes.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Shares context between all tests decorated with Collection <see cref="FixtureNames.SharedContextFixture"/>.
    /// It's called only once before very first test is executed to create the database and apply all required configurations.
    /// </summary>
    public class SharedContextTestFixture
    {
        public SharedContextTestFixture()
        {
            var configuration = CreateConfiguration();
            
            SetCurrentThreadCulture(configuration);

            ConfigureDependencies(configuration);
            
            DropAndCreateDatabase(configuration);
        }

        private static IConfiguration CreateConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Testing.json")
                .Build();

            return config;
        }

        private static void DropAndCreateDatabase(IConfiguration configuration)
        {
            var connectionString = SQLiteConnectionStringParser.Parse(configuration.GetConnectionString("DefaultConnection"));
            var connectionFactory = new DefaultConnectionFactory(connectionString);

            var connectionValues = connectionString.ToNameValueCollection();
            var dbFilePath = connectionValues.Get(SQLiteConnectionStringKeys.DataSource);

            File.Delete(dbFilePath);

            DatabaseMigrator.Migrate(connectionFactory);
        }

        private static void ConfigureDependencies(IConfiguration configuration)
        {
            var builder = AutofacConfig.Configure(new ContainerBuilder(), configuration);
            builder.RegisterType<FakeInteractionService>().As<IInteractionService>().SingleInstance();
            builder.RegisterType<FakeEventAggregator>().As<IEventAggregator>().SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetContainer(container);
        }

        private static void SetCurrentThreadCulture(IConfiguration configuration)
        {
            var locale = configuration.GetValue<string>("AppLocale");
            var culture = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
