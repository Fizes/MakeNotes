using System;

namespace MakeNotes.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Used to resolve dependencies in tests.
    /// It must have limited use, normally dependencies aren't created explicitly in tests.
    /// </summary>
    public static class DependencyResolver
    {
        private static Func<Type, object> _resolver;

        internal static void SetResolver(Func<Type, object> resolver)
        {
            if (_resolver != null)
            {
                throw new InvalidOperationException("Dependency resolver can be set only once");
            }

            _resolver = resolver;
        }
        
        public static T Resolve<T>()
        {
            return (T)_resolver(typeof(T));
        }
    }
}
