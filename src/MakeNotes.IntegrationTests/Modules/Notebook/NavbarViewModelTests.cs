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
    public class NavbarViewModelTests : TransactionalTestFixture
    {
        private readonly NavbarViewModel _viewModel;

        public NavbarViewModelTests()
        {
            _viewModel = DependencyResolver.Resolve<NavbarViewModel>();
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
            _viewModel.LoadTabsCommand.Execute();
            Assert.Single(_viewModel.Tabs);

            var tab1 = await TabGenerator.CreateTab(order: 1);
            var tab2 = await TabGenerator.CreateTab(order: 2);

            _viewModel.LoadTabsCommand.Execute();

            AssertItemInCollection(_viewModel.Tabs, tab1);
            AssertItemInCollection(_viewModel.Tabs, tab2);
        }

        [Fact]
        public void AddTabCommand_ShouldAddTabs()
        {
            // 1. Adding an item with particular name
            FakeInteractionService.DialogResult = DialogResult.Accepted;
            var tabName = Guid.NewGuid().ToString("N");
            _viewModel.TabName = tabName;

            _viewModel.LoadTabsCommand.Execute();
            _viewModel.AddTabCommand.Execute();

            var newTab = _viewModel.SelectedTab;
            var initialTab = _viewModel.Tabs[0];
            Assert.Null(_viewModel.TabName);
            Assert.Equal(tabName, newTab.Header);
            // Default item is first and given id
            AssertItemInCollection(_viewModel.Tabs, initialTab.Id, DefaultValues.DefaultTabName, initialTab.Order);
            AssertItemInCollection(_viewModel.Tabs, newTab.Id, tabName, newTab.Order);

            // 2. Adding an item without name
            FakeInteractionService.DialogResult = DialogResult.Accepted;
            _viewModel.AddTabCommand.Execute();

            newTab = _viewModel.SelectedTab;
            AssertItemInCollection(_viewModel.Tabs, newTab.Id, DefaultValues.DefaultTabName, newTab.Order);
        }

        [Fact]
        public async Task DeleteTabCommand_ShouldDeleteTabs_AndPreserveInitialDefaultTab()
        {
            var tab1 = await TabGenerator.CreateTab(order: 0);
            var tab2 = await TabGenerator.CreateTab(order: 1);
            _viewModel.LoadTabsCommand.Execute();

            // 1. Deleting the first item
            var tabItem1 = _viewModel.Tabs.First(t => t.Id == tab1.Id);
            var tabItem2 = _viewModel.Tabs.First(t => t.Id == tab2.Id);
            _viewModel.DeleteTabCommand.Execute(tabItem1);

            Assert.DoesNotContain(_viewModel.Tabs, t => t.Id == tab1.Id);

            // 2. Deleting the second item
            _viewModel.DeleteTabCommand.Execute(tabItem2);

            Assert.DoesNotContain(_viewModel.Tabs, t => t.Id == tab2.Id);

            // 3. Initial default tab is preserved
            Assert.Single(_viewModel.Tabs);
            Assert.Equal(DefaultValues.DefaultTabName, _viewModel.Tabs[0].Header);

            // 4. Making sure the tabs were deleted after reloading
            _viewModel.LoadTabsCommand.Execute();

            Assert.Single(_viewModel.Tabs);
        }
    }
}
