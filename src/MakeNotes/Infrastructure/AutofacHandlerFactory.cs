using System.Collections.Generic;
using Autofac;
using MakeNotes.Common.Core;

namespace MakeNotes.Infrastructure
{
    /// <summary>
    /// Autofac implementation of <see cref="IHandlerFactory"/>.
    /// </summary>
    public class AutofacHandlerFactory : IHandlerFactory
    {
        private readonly IComponentContext _componentContext;

        public AutofacHandlerFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public THandler Create<THandler>() where THandler : IHandler
        {
            return _componentContext.Resolve<THandler>();
        }

        public IEnumerable<TNotificationHandler> CreateAll<TNotificationHandler>() where TNotificationHandler : IHandler
        {
            return _componentContext.Resolve<IEnumerable<TNotificationHandler>>();
        }
    }
}
