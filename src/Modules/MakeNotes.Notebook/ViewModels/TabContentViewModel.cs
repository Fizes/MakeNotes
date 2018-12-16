using System.Collections.Generic;
using MakeNotes.Notebook.Models;
using MakeNotes.Notebook.Providers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MakeNotes.Notebook.ViewModels
{
    public class TabContentViewModel : BindableBase
    {
        private readonly INavigationContext _navigationContext;
        private readonly IRegionManager _regionManager;

        public TabContentViewModel(INavigationContext navigationContext, IRegionManager regionManager)
        {
            _navigationContext = navigationContext;
            _regionManager = regionManager;
            InitializeActionMenuItems();
        }

        public IEnumerable<ActionMenuItem> ActionMenuItems { get; private set; }

        private void InitializeActionMenuItems()
        {
            var addGridAction = new ActionMenuItem
            {
                Tooltip = "Add new grid",
                Icon = "TablePlus",
                Action = new DelegateCommand(AddNewGrid)
            };

            ActionMenuItems = new ActionMenuItem[]
            {
                addGridAction
            };
        }

        private void AddNewGrid()
        {
            var grid = new Views.Templates.DataGridTemplate();
            _regionManager.Regions["TabContentRegion"].Add(grid);
        }
    }
}
