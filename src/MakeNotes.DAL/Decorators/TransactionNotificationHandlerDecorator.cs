using Dapper.AmbientContext;
using MakeNotes.Common.Core.Notifications;

namespace MakeNotes.DAL.Decorators
{
    /// <summary>
    /// Executes the decorated handler within a transaction.
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public class TransactionNotificationHandlerDecorator<TNotification> : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        private readonly IAmbientDbContextFactory _ambientDbContextFactory;
        private readonly INotificationHandler<TNotification> _decorated;

        public TransactionNotificationHandlerDecorator(IAmbientDbContextFactory ambientDbContextFactory, INotificationHandler<TNotification> decorated)
        {
            _ambientDbContextFactory = ambientDbContextFactory;
            _decorated = decorated;
        }

        public void Handle(TNotification command)
        {
            using (var ambientContext = _ambientDbContextFactory.Create())
            {
                try
                {
                    _decorated.Handle(command);
                    ambientContext.Commit();
                }
                catch
                {
                    ambientContext.Rollback();
                    throw;
                }
            }
        }
    }
}
