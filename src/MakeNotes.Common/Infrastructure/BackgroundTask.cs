using System;
using System.ComponentModel;
using MakeNotes.Common.Interfaces;

namespace MakeNotes.Common.Infrastructure
{
    public class BackgroundTask : IBackgroundTask
    {
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        private Action _action;

        public BackgroundTask()
        {
            _backgroundWorker.DoWork += (s, e) => _action?.Invoke();
        }

        public void Start(Action action)
        {
            _action = action;
            _backgroundWorker.RunWorkerAsync();
        }
    }
}
