using System;
using System.Linq;
using System.Threading.Tasks;
using MakeNotes.Common.Infrastructure.Extensions;
using MakeNotes.DAL.Models;
using MakeNotes.Framework.Models;
using MakeNotes.IntegrationTests.Infrastructure;
using MakeNotes.IntegrationTests.Infrastructure.DataGenerators;
using MakeNotes.IntegrationTests.Infrastructure.Fakes;
using MakeNotes.Notebook.Collections;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.ViewModels;
using Xunit;

namespace MakeNotes.IntegrationTests.Modules.Notebook
{
    [Collection(FixtureNames.SharedContextFixture)]
    public class NavbarViewModelTests
    {
        private readonly NavbarViewModel _navbarViewModel;

        public NavbarViewModelTests()
        {
            _navbarViewModel = DependencyResolver.Resolve<NavbarViewModel>();
        }

        private static void AssertItemInCollection(NavbarTabItemObservableCollection collection, Tab tabItem)
        {
            AssertItemInCollection(collection, tabItem.Id, tabItem.Name, tabItem.Order);
        }

        private static void AssertItemInCollection(NavbarTabItemObservableCollection collection, int id, string name, int order)
        {
            Assert.Contains(collection, t => t.Id == id && t.Header == name && t.Order == order);
        }

        [Fact]
        public async Task LoadTabsCommand_ShouldLoadAllTabs()
        {
            _navbarViewModel.LoadTabsCommand.Execute(null);

            Assert.Single(_navbarViewModel.Tabs);

            using (new ExecuteWithRollback())
            {
                var tab1 = await TabGenerator.CreateTab(order: 0);
                var tab2 = await TabGenerator.CreateTab(order: 1);

                _navbarViewModel.LoadTabsCommand.Execute(null);

                AssertItemInCollection(_navbarViewModel.Tabs, tab1);
                AssertItemInCollection(_navbarViewModel.Tabs, tab2);
            }
        }

        [Fact]
        public void AddTabCommand_ShouldAddTabs()
        {
            // 1. Adding an item with particular name
            FakeInteractionService.DialogResult = DialogResult.Accepted;
            var tabName = Guid.NewGuid().ToString("N");
            _navbarViewModel.TabName = tabName;

            using (new ExecuteWithRollback())
            {
                _navbarViewModel.AddTabCommand.Execute(null);

                var newTab = _navbarViewModel.SelectedTab;
                var initialTab = _navbarViewModel.Tabs.GetPreviousElement(newTab);
                Assert.Null(_navbarViewModel.TabName);
                Assert.Equal(tabName, newTab.Header);
                // Default item is first and given id
                AssertItemInCollection(_navbarViewModel.Tabs, initialTab.Id.Value, DefaultValues.DefaultTabName, initialTab.Order);
                AssertItemInCollection(_navbarViewModel.Tabs, newTab.Id.Value, tabName, newTab.Order);

                // 2. Adding an item without name
                FakeInteractionService.DialogResult = DialogResult.Accepted;
                _navbarViewModel.AddTabCommand.Execute(null);

                newTab = _navbarViewModel.SelectedTab;
                AssertItemInCollection(_navbarViewModel.Tabs, newTab.Id.Value, DefaultValues.DefaultTabName, newTab.Order);
            }
        }

        [Fact]
        public async Task DeleteTabCommand_ShouldDeleteTabs()
        {
            var tab1 = await TabGenerator.CreateTab(order: 0);
            var tab2 = await TabGenerator.CreateTab(order: 1);
            _navbarViewModel.LoadTabsCommand.Execute(null);

            // 1. Deleting first item
            var tabItem1 = _navbarViewModel.Tabs.First(t => t.Id == tab1.Id);
            var tabItem2 = _navbarViewModel.Tabs.First(t => t.Id == tab2.Id);
            _navbarViewModel.DeleteTabCommand.Execute(tabItem1);

            Assert.DoesNotContain(_navbarViewModel.Tabs, t => t.Id == tab1.Id);

            // 2. Deleting second item
            _navbarViewModel.DeleteTabCommand.Execute(tabItem2);

            Assert.DoesNotContain(_navbarViewModel.Tabs, t => t.Id == tab2.Id);

            // 3. Making sure the tabs were deleted after reloading
            _navbarViewModel.LoadTabsCommand.Execute(null);

            Assert.DoesNotContain(_navbarViewModel.Tabs, t => t.Id == tab1.Id);
            Assert.DoesNotContain(_navbarViewModel.Tabs, t => t.Id == tab2.Id);
        }
    }
}
