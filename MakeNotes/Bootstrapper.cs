using System;
using System.Reflection;
using System.Windows;
using Autofac;
using MakeNotes.Framework.Utilities;
using MakeNotes.Infrastructure;
using MakeNotes.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Prism.Autofac;
using Prism.Ioc;

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
            
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PublicOnly();

            builder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());

            builder.RegisterInstance(_configuration.GetConfiguration<WindowSettings>());
        }
        
        protected override void InitializeModules()
        {
        }
    }
}
