using System;
using System.Linq;
using MakeNotes.Common.Models;
using MakeNotes.Framework.Models;
using MakeNotes.IntegrationTests.Infrastructure;
using MakeNotes.IntegrationTests.Infrastructure.Fakes;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Models;
using MakeNotes.Notebook.ViewModels;
using Xunit;

namespace MakeNotes.IntegrationTests.Modules.Notebook
{
    [Collection(FixtureNames.SharedContextFixture)]
    public class TabContentViewModelTests : TransactionalTestFixture
    {
        private readonly TabContentViewModel _viewModel;
        private readonly NavbarViewModel _navbarViewModel;

        public TabContentViewModelTests()
        {
            _viewModel = DependencyResolver.Resolve<TabContentViewModel>();
            _navbarViewModel = DependencyResolver.Resolve<NavbarViewModel>();
        }

        private ActionMenuItem GetActionMenuItemByName(string templateName)
        {
            return _viewModel.ActionMenuItems.First(a => (string)a.ActionParameter == templateName);
        }

        private NavbarTabItem CreateTab()
        {
            FakeInteractionService.DialogResult = DialogResult.Accepted;
            var tabName = Guid.NewGuid().ToString("N");
            _navbarViewModel.TabName = tabName;
            _navbarViewModel.AddTabCommand.Execute();

            return _navbarViewModel.Tabs.Single(t => t.Header == tabName);
        }

        [Fact]
        public void ActionMenuItems_ShouldContainElements_WhenViewModelIsInitialized()
        {
            Assert.NotEmpty(_viewModel.ActionMenuItems);
        }

        [Fact]
        public void PasswordSheet_ShouldBeCreated_WhenSelectedInActionMenuItems()
        {
            Assert.Empty(_viewModel.Content);
            _navbarViewModel.LoadTabsCommand.Execute();

            var templateName = VisualBlockTypes.PasswordSheet;
            var addPasswordSheetItem = GetActionMenuItemByName(templateName);

            addPasswordSheetItem.Action.Execute(templateName);

            Assert.Contains(_viewModel.Content, c => c.TemplateName.Contains(templateName));
        }

        [Fact]
        public void TabContent_ShouldBeLoaded_WhenTabIsSelected()
        {
            Assert.Equal(0, _viewModel.CurrentTabId);

            var tabWithContent = CreateTab();
            var emptyTab = CreateTab();

            // 1. Selecting tab that will have content
            _navbarViewModel.SelectedTab = tabWithContent;
            Assert.Equal(tabWithContent.Id, _viewModel.CurrentTabId);

            // 2. Adding content to the tab
            var templateName = VisualBlockTypes.PasswordSheet;
            var addPasswordSheetItem = GetActionMenuItemByName(templateName);
            addPasswordSheetItem.Action.Execute(templateName);

            // 3. Switching to another tab without content
            _navbarViewModel.SelectedTab = emptyTab;
            Assert.Equal(emptyTab.Id, _viewModel.CurrentTabId);
            Assert.Empty(_viewModel.Content);

            // 4. Switching back to the tab with content
            _navbarViewModel.SelectedTab = tabWithContent;
            Assert.Equal(tabWithContent.Id, _viewModel.CurrentTabId);
            Assert.NotEmpty(_viewModel.Content);
        }
    }
}
