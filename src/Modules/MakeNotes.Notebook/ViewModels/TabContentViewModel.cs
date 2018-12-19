using System.Collections.Generic;
using System.Collections.ObjectModel;
using MakeNotes.Common.Models;
using MakeNotes.Framework.Factories;
using MakeNotes.Notebook.Models;
using MakeNotes.Notebook.Providers;
using MakeNotes.Notebook.Views.Templates;
using Prism.Commands;
using Prism.Mvvm;

namespace MakeNotes.Notebook.ViewModels
{
    public class TabContentViewModel : BindableBase
    {
        private readonly INavigationContext _navigationContext;
        private readonly IViewFactory _viewFactory;

        public TabContentViewModel(INavigationContext navigationContext, IViewFactory viewFactory)
        {
            _navigationContext = navigationContext;
            _viewFactory = viewFactory;
            
            ActionMenuItems = new ActionMenuItem[]
            {
                new ActionMenuItem
                {
                    Tooltip = "Add a new password list",
                    Icon = "TablePlus",
                    Action = new DelegateCommand(AddNewGrid)
                }
            };
        }

        public IEnumerable<ActionMenuItem> ActionMenuItems { get; }

        public ObservableCollection<IDynamicElement> Content { get; } = new ObservableCollection<IDynamicElement>();

        private void AddNewGrid()
        {
            var grid = _viewFactory.Create<PasswordDataGridTemplate>();
            Content.Add(grid);
        }
    }
}
