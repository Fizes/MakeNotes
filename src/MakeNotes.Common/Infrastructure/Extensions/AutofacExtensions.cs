using System;
using System.Linq;
using Autofac.Features.Decorators;

namespace Autofac
{
    public static class AutofacExtensions
    {
        #region Utils
        private static void ThrowIfTypeImplementsMoreThanOneSameInterface(IDecoratorContext context)
        {
            var serviceType = context.ServiceType;
            if (!serviceType.IsInterface)
            {
                return;
            }

            var implementedInterfacesCount = context.ImplementationType
                .GetInterfaces()
                .Count(t => t.IsGenericType && t.GetGenericTypeDefinition() == serviceType.GetGenericTypeDefinition());

            if (implementedInterfacesCount > 1)
            {
                throw new InvalidOperationException("Decorator cannot be created for type implementing multiple generic interfaces of the same type");
            }
        }

        private static bool IsTypeAssignableFrom<TService>(IDecoratorContext context)
        {
            ThrowIfTypeImplementsMoreThanOneSameInterface(context);
            return context.ServiceType == typeof(TService);
        }
        #endregion

        /// <summary>
        /// Decorates all components implementing open generic service type.
        /// <remarks>
        /// Decorated type should be implemented by a class that implements only one generic type of the same type
        /// since Autofac is not able to properly create a decorator if multiple generic types implemented in a single class.
        /// </remarks>
        /// </summary>
        /// <typeparam name="TImplementer">The type of the service implementation.</typeparam>
        /// <typeparam name="TService">Service type being decorated. Must be an open generic type.</typeparam>
        /// <param name="builder">Container builder.</param>
        /// <param name="decoratorType">The type of the decorator. Must be an open generic type, and accept a parameter
        /// of type <typeparamref name="TService" />, which will be set to the instance being decorated.</param>
        public static void RegisterGenericDecorator<TService>(this ContainerBuilder builder, Type decoratorType)
        {
            var serviceType = typeof(TService);
            if (!serviceType.IsGenericType)
            {
                throw new ArgumentException("Service type should be generic type");
            }

            builder.RegisterGenericDecorator(
                decoratorType,
                serviceType.GetGenericTypeDefinition(),
                context => IsTypeAssignableFrom<TService>(context));
        }
    }
}
