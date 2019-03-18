using System;
using System.Threading;
using MakeNotes.Common.Interfaces;

namespace MakeNotes.Common.Infrastructure
{
    public class BackgroundTask : IBackgroundTask
    {
        public void Start(Action action)
        {
            ThreadPool.QueueUserWorkItem(state => action?.Invoke());
        }
    }
}
