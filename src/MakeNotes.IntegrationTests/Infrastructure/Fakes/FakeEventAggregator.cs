using Prism.Events;

namespace MakeNotes.IntegrationTests.Infrastructure.Fakes
{
    class FakeEventAggregator : IEventAggregator
    {
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            return new TEventType();
        }
    }
}
