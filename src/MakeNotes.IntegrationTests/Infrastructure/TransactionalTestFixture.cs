using System;

namespace MakeNotes.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Runs tests inside transaction scope and rollbacks any changes after tests are finished.
    /// </summary>
    public class TransactionalTestFixture : IDisposable
    {
        private readonly ExecuteWithRollback _runTestsWithRollback;

        public TransactionalTestFixture()
        {
            _runTestsWithRollback = new ExecuteWithRollback();
        }

        public void Dispose()
        {
            _runTestsWithRollback.Dispose();
        }
    }
}
