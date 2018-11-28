using System;
using System.Reflection;
using Autofac;

namespace MakeNotes.Infrastructure
{
    /// <summary>
    /// DI-container setup.
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Registers all dependencies and returns the container instance.
        /// </summary>
        /// <returns></returns>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PublicOnly();

            builder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());

            return builder.Build();
        }
    }
}
