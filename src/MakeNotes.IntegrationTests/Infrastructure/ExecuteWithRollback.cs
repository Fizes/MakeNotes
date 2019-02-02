using System;
using Dapper.AmbientContext;

namespace MakeNotes.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Rollbacks any changes to the database after <see cref="Dispose"/> is called.
    /// Should be used when made changes that may affect other tests.
    /// </summary>
    public class ExecuteWithRollback : IDisposable
    {
        private readonly IAmbientDbContext _ambientDbContext;

        public ExecuteWithRollback()
        {
            var ambientContextFactory = DependencyResolver.Resolve<IAmbientDbContextFactory>();
            _ambientDbContext = ambientContextFactory.Create(join: false);
        }

        public void Dispose()
        {
            _ambientDbContext.Rollback();
            _ambientDbContext.Dispose();
        }
    }
}
