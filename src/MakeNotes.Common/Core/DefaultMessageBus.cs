using System;
using System.Reflection;
using System.Threading.Tasks;
using MakeNotes.Common.Core.Factories;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Default implementation of <see cref="IMessageBus"/> that executes actions in-memory.
    /// </summary>
    public class DefaultMessageBus : IMessageBus
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly INotificationStrategyFactory _notificationStrategyFactory;

        private static readonly MethodInfo SendMethod;
        private static readonly MethodInfo PublishMethod;

        static DefaultMessageBus()
        {
            SendMethod = typeof(DefaultMessageBus).GetMethod(nameof(SendCore), BindingFlags.NonPublic | BindingFlags.Instance);
            PublishMethod = typeof(DefaultMessageBus).GetMethod(nameof(PublishCore), BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public DefaultMessageBus(IHandlerFactory handlerFactory, INotificationStrategyFactory notificationStrategyFactory)
        {
            _handlerFactory = handlerFactory;
            _notificationStrategyFactory = notificationStrategyFactory;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // A little reflection hack to call a generic method with multiple parameters
            // via a single parameter generic method
            var requestType = request.GetType();
            var method = SendMethod.MakeGenericMethod(requestType, typeof(TResponse));

            var result = await (Task<TResponse>)method.Invoke(this, new[] { request });

            Publish();

            return result;
        }
        
        public void Publish()
        {
            while (ApplicationEvents.Notifications.TryDequeue(out var notification))
            {
                var notificationType = notification.GetType();
                var method = PublishMethod.MakeGenericMethod(notificationType);

                method.Invoke(this, new[] { notification });
            }
        }

        public void Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            PublishCore(notification);
        }

        private Task<TResult> SendCore<TRequest, TResult>(TRequest query) where TRequest : IRequest<TResult>
        {
            var handler = _handlerFactory.Create<IRequestHandler<TRequest, TResult>>();
            return handler.ExecuteAsync(query);
        }

        private void PublishCore<TNotification>(TNotification notification) where TNotification : INotification
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            var strategy = _notificationStrategyFactory.Create<FireAndForgetNotificationStrategy>();
            strategy.Publish(notification);
        }
    }
}
