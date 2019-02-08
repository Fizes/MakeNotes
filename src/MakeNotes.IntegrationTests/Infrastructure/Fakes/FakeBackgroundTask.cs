using System;
using MakeNotes.Common.Interfaces;

namespace MakeNotes.IntegrationTests.Infrastructure.Fakes
{
    public class FakeBackgroundTask : IBackgroundTask
    {
        public void Start(Action action)
        {
            action?.Invoke();
        }
    }
}
