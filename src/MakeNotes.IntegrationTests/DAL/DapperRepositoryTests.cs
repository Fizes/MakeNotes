using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper.AmbientContext;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Models;
using MakeNotes.DAL.Queries;
using MakeNotes.IntegrationTests.Infrastructure;
using Xunit;

namespace MakeNotes.IntegrationTests.DAL
{
    [Collection(FixtureNames.SharedContextFixture)]
    public class DapperRepositoryTests
    {
        [Fact]
        public async Task ChildContext_ShouldJoinParentContext_WhenNested()
        {
            var ambientContextFactory = DependencyResolver.Resolve<IAmbientDbContextFactory>();
            var repository = DependencyResolver.Resolve<IRepository>();

            using (var context = ambientContextFactory.Create())
            {
                var tab1Name = $"Tab1 {new Guid().ToString()}";
                var tab2Name = $"Tab2 {new Guid().ToString()}";

                try
                {
                    await repository.ExecuteAsync(new QueryObject(TabQueries.CreateTab, new { Name = tab1Name, Order = 0 }));

                    await repository.ExecuteAsync(new QueryObject(TabQueries.CreateTab, new { Name = tab2Name, Order = 1 }));

                    var tabs = await repository.QueryAsync<Tab>(new QueryObject(TabQueries.GetAllTabs));
                    tabs = tabs.Where(t => t.Name == tab1Name || t.Name == tab2Name);

                    Assert.Equal(2, tabs.Count());

                    throw new Exception();
                }
                catch
                {
                    context.Rollback();

                    var tabs = await repository.QueryAsync<Tab>(new QueryObject(TabQueries.GetAllTabs));
                    tabs = tabs.Where(t => t.Name == tab1Name || t.Name == tab2Name);

                    Assert.Empty(tabs);
                }
            }
        }
    }
}
