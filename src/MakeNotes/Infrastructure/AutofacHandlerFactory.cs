using Autofac;
using MakeNotes.Common.Core;
using MakeNotes.Common.Core.Commands;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Core.Queries;

namespace MakeNotes.Infrastructure
{
    public class AutofacHandlerFactory : IHandlerFactory
    {
        private readonly IComponentContext _componentContext;

        public AutofacHandlerFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public IQueryHandler<IQuery<TResult>, TResult> Create<TResult>(IQuery<TResult> query)
        {
            return _componentContext.Resolve<IQueryHandler<IQuery<TResult>, TResult>>();
        }

        public ICommandHandler<ICommand> Create(ICommand command)
        {
            return _componentContext.Resolve<ICommandHandler<ICommand>>();
        }

        public INotificationHandler<INotification> Create(INotification notification)
        {
            return _componentContext.Resolve<INotificationHandler<INotification>>();
        }
    }
}
