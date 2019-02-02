using System;
using Autofac;

namespace MakeNotes.Common.Infrastructure.Extensions
{
    public static class AutofacExtensions
    {
        /// <summary>
        /// Decorate all components implementing open generic service type.
        /// </summary>
        /// <typeparam name="TImplementer">The type of the service implementation.</typeparam>
        /// <typeparam name="TService">Service type being decorated. Must be an open generic type.</typeparam>
        /// <param name="builder">Container builder.</param>
        /// <param name="decoratorType">The type of the decorator. Must be an open generic type, and accept a parameter
        /// of type <typeparamref name="TService" />, which will be set to the instance being decorated.</param>
        public static void RegisterGenericDecorator<TImplementer, TService>(this ContainerBuilder builder, Type decoratorType)
        {
            builder.RegisterType<TImplementer>().Named<TService>(nameof(TImplementer));
            builder.RegisterGenericDecorator(
                decoratorType,
                typeof(TService).GetGenericTypeDefinition(),
                fromKey: nameof(TImplementer));
        }
    }
}
