using System;
using Autofac;

namespace MakeNotes.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Used to resolve dependencies in tests.
    /// It must have limited use, normally dependencies aren't created explicitly in tests.
    /// </summary>
    public static class DependencyResolver
    {
        static IContainer _container;

        internal static void SetContainer(IContainer container)
        {
            if (_container != null)
            {
                throw new InvalidOperationException("Dependency resolver can be set only once");
            }

            _container = container;
        }
        
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
