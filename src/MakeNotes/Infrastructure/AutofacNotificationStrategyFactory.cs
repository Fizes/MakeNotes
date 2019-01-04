using Autofac;
using MakeNotes.Common.Core.Factories;
using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.Infrastructure
{
    /// <summary>
    /// Autofac implementation of <see cref="INotificationStrategyFactory"/>.
    /// </summary>
    public class AutofacNotificationStrategyFactory : INotificationStrategyFactory
    {
        private readonly IComponentContext _componentContext;

        public AutofacNotificationStrategyFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public TNotificationStrategy Create<TNotificationStrategy>() where TNotificationStrategy : INotificationStrategy
        {
            return _componentContext.Resolve<TNotificationStrategy>();
        }
    }
}
