using MakeNotes.Common.Core.Notifications;
using MakeNotes.DAL.Core;

namespace MakeNotes.DAL.Decorators
{
    /// <summary>
    /// Executes the decorated handler within a transaction.
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public class TransactionNotificationHandlerDecorator<TNotification> : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        private readonly IRepository _repository;
        private readonly INotificationHandler<TNotification> _decorated;

        public TransactionNotificationHandlerDecorator(IRepository repository, INotificationHandler<TNotification> decorated)
        {
            _repository = repository;
            _decorated = decorated;
        }

        public void Handle(TNotification command)
        {
            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    _decorated.Handle(command);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
