﻿using System.Threading;
using MakeNotes.Common.Infrastructure;
using Xunit;

namespace MakeNotes.UnitTests.Common
{
    public class BackgroundTaskTests
    {
        private static void ImitateWork()
        {
            Thread.Sleep(1000);
        }

        [Fact]
        public void Start_ShouldNotThrowException_WhenMultipleTasksStarted()
        {
            var task = new BackgroundTask();

            task.Start(ImitateWork);
            task.Start(ImitateWork);
            task.Start(ImitateWork);
        }
    }
}
