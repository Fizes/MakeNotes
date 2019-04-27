using System;
using MakeNotes.Notebook.Models;
using MakeNotes.Notebook.Templates.VisualBlocks.ViewModels;

namespace MakeNotes.Notebook.Templates
{
    public class VisualBlockTemplateFactory
    {
        private readonly Func<string, IVisualBlockViewModel> _viewModelFactory;

        public VisualBlockTemplateFactory(Func<string, IVisualBlockViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public VisualBlockTemplate Create(int tabContentId, string templateName)
        {
            return new VisualBlockTemplate
            {
                TabContentId = tabContentId,
                TemplateName = $"{templateName}Template",
                DataContext = _viewModelFactory(templateName)
            };
        }
    }
}
