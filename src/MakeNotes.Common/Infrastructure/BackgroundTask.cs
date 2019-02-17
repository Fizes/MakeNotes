using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MakeNotes.Common.Interfaces;

namespace MakeNotes.Common.Infrastructure
{
    public class BackgroundTask : IBackgroundTask
    {
        private readonly List<BackgroundWorker> _backgroundWorkers = new List<BackgroundWorker>();

        public void Start(Action action)
        {
            var backgroundWorker = GetFreeBackgroundWorker();
            backgroundWorker.RunWorkerAsync(action);
        }

        private BackgroundWorker GetFreeBackgroundWorker()
        {
            var backgroundWorker = _backgroundWorkers.FirstOrDefault(bw => !bw.IsBusy);
            if (backgroundWorker == null)
            {
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += OnWorkerRunning;
                backgroundWorker.RunWorkerCompleted += OnRunWorkerCompleted;

                _backgroundWorkers.Add(backgroundWorker);
            }

            return backgroundWorker;
        }

        private void OnWorkerRunning(object sender, DoWorkEventArgs e)
        {
            var action = (Action)e.Argument;
            action?.Invoke();
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_backgroundWorkers.Count < 2)
            {
                return;
            }

            var backgroundWorker = (BackgroundWorker)sender;
            backgroundWorker.DoWork -= OnWorkerRunning;
            backgroundWorker.RunWorkerCompleted -= OnRunWorkerCompleted;
            _backgroundWorkers.Remove(backgroundWorker);
        }
    }
}
