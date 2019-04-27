using System;
using System.Collections.Generic;
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
        private static IEnumerable<Tab> FilterTabsByNames(IEnumerable<Tab> tabs, params string[] tabNames)
        {
            return tabs.Where(t => tabNames.Any(tn => t.Name == tn)).ToArray();
        }

        [Fact]
        public async Task ChildContext_ShouldJoinParentContext_WhenNested()
        {
            var repository = DependencyResolver.Resolve<IRepository>();
            var ambientContextFactory = DependencyResolver.Resolve<IAmbientDbContextFactory>();

            using (var context = ambientContextFactory.Create())
            {
                var tab1Name = $"Tab1 {new Guid().ToString()}";
                var tab2Name = $"Tab2 {new Guid().ToString()}";
                var tabNames = new[] { tab1Name, tab2Name };

                try
                {
                    await repository.ExecuteAsync(new QueryObject(TabQueries.CreateTab, new { Name = tab1Name, Order = 0 }));
                    await repository.ExecuteAsync(new QueryObject(TabQueries.CreateTab, new { Name = tab2Name, Order = 1 }));

                    var tabs = await repository.QueryAsync<Tab>(new QueryObject(TabQueries.GetAllTabs));
                    tabs = FilterTabsByNames(tabs, tabNames);

                    Assert.Equal(2, tabs.Count());

                    throw new Exception();
                }
                catch
                {
                    context.Rollback();

                    var tabs = await repository.QueryAsync<Tab>(new QueryObject(TabQueries.GetAllTabs));
                    tabs = FilterTabsByNames(tabs, tabNames);

                    Assert.Empty(tabs);
                }
            }
        }
    }
}
