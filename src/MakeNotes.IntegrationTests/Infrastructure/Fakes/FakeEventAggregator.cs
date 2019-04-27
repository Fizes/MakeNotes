using System;
using System.Collections.Generic;
using System.Threading;
using Prism.Events;

namespace MakeNotes.IntegrationTests.Infrastructure.Fakes
{
    public class FakeEventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, EventBase> _events = new Dictionary<Type, EventBase>();
        private readonly SynchronizationContext _syncContext = new FakeSynchronizationContext();
        
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            lock (_events)
            {
                if (!_events.TryGetValue(typeof(TEventType), out EventBase eventBase))
                {
                    var value = new TEventType();
                    value.SynchronizationContext = _syncContext;
                    _events[typeof(TEventType)] = value;
                    return value;
                }

                return (TEventType)eventBase;
            }
        }
    }
}
