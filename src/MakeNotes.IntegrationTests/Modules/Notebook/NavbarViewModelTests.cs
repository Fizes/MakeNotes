using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MakeNotes.Common.Models;
using MakeNotes.DAL.Models;
using MakeNotes.Framework.Models;
using MakeNotes.IntegrationTests.Infrastructure;
using MakeNotes.IntegrationTests.Infrastructure.DataGenerators;
using MakeNotes.IntegrationTests.Infrastructure.Fakes;
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

        private static void AssertItemInCollection(ObservableCollection<NavbarTabItem> collection, Tab tabItem)
        {
            AssertItemInCollection(collection, tabItem.Id, tabItem.Name, tabItem.Order);
        }

        private static void AssertItemInCollection(ObservableCollection<NavbarTabItem> collection, int id, string name, int order)
        {
            Assert.Contains(collection, t => t.Id == id && t.Header == name && t.Order == order);
        }

        [Fact]
        public async Task LoadTabsCommand_ShouldLoadAllTabsWithInitialDefaultTab()
        {
            using (new ExecuteWithRollback())
            {
                _navbarViewModel.LoadTabsCommand.Execute();
                Assert.Single(_navbarViewModel.Tabs);

                var tab1 = await TabGenerator.CreateTab(order: 1);
                var tab2 = await TabGenerator.CreateTab(order: 2);

                _navbarViewModel.LoadTabsCommand.Execute();

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
                _navbarViewModel.LoadTabsCommand.Execute();
                _navbarViewModel.AddTabCommand.Execute();

                var newTab = _navbarViewModel.SelectedTab;
                var initialTab = _navbarViewModel.Tabs[0];
                Assert.Null(_navbarViewModel.TabName);
                Assert.Equal(tabName, newTab.Header);
                // Default item is first and given id
                AssertItemInCollection(_navbarViewModel.Tabs, initialTab.Id, DefaultValues.DefaultTabName, initialTab.Order);
                AssertItemInCollection(_navbarViewModel.Tabs, newTab.Id, tabName, newTab.Order);

                // 2. Adding an item without name
                FakeInteractionService.DialogResult = DialogResult.Accepted;
                _navbarViewModel.AddTabCommand.Execute();

                newTab = _navbarViewModel.SelectedTab;
                AssertItemInCollection(_navbarViewModel.Tabs, newTab.Id, DefaultValues.DefaultTabName, newTab.Order);
            }
        }

        [Fact]
        public async Task DeleteTabCommand_ShouldDeleteTabs_AndPreserveInitialDefaultTab()
        {
            using (new ExecuteWithRollback())
            {
                var tab1 = await TabGenerator.CreateTab(order: 0);
                var tab2 = await TabGenerator.CreateTab(order: 1);
                _navbarViewModel.LoadTabsCommand.Execute();

                // 1. Deleting the first item
                var tabItem1 = _navbarViewModel.Tabs.First(t => t.Id == tab1.Id);
                var tabItem2 = _navbarViewModel.Tabs.First(t => t.Id == tab2.Id);
                _navbarViewModel.DeleteTabCommand.Execute(tabItem1);

                Assert.DoesNotContain(_navbarViewModel.Tabs, t => t.Id == tab1.Id);

                // 2. Deleting the second item
                _navbarViewModel.DeleteTabCommand.Execute(tabItem2);
                
                Assert.DoesNotContain(_navbarViewModel.Tabs, t => t.Id == tab2.Id);

                // 3. Initial default tab is preserved
                Assert.Single(_navbarViewModel.Tabs);
                Assert.Equal(DefaultValues.DefaultTabName, _navbarViewModel.Tabs[0].Header);

                // 4. Making sure the tabs were deleted after reloading
                _navbarViewModel.LoadTabsCommand.Execute();

                Assert.Single(_navbarViewModel.Tabs);
            }
        }
    }
}
