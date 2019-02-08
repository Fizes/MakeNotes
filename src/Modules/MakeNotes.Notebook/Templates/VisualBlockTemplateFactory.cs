using Autofac;
using MakeNotes.Notebook.Models;
using MakeNotes.Notebook.Templates.VisualBlocks.ViewModels;

namespace MakeNotes.Notebook.Templates
{
    public class VisualBlockTemplateFactory
    {
        private readonly IComponentContext _componentContext;

        public VisualBlockTemplateFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public VisualBlockTemplate Create(int tabContentId, string templateName)
        {
            return new VisualBlockTemplate
            {
                TabContentId = tabContentId,
                TemplateName = $"{templateName}Template",
                DataContext = _componentContext.ResolveNamed<IVisualBlockViewModel>(templateName)
            };
        }
    }
}
