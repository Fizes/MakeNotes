using System.Threading;

namespace MakeNotes.IntegrationTests.Infrastructure.Fakes
{
    public class FakeSynchronizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            d.Invoke(state);
        }
    }
}
