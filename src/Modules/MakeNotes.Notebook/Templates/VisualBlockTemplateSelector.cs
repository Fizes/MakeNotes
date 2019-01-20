using System.Windows;
using System.Windows.Controls;
using MakeNotes.Notebook.Models;

namespace MakeNotes.Notebook.Templates.VisualBlocks
{
    /// <summary>
    /// Finds and returns a visual block template by provided info.
    /// </summary>
    public class VisualBlockTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is VisualBlockTemplate visualBlockTemplate))
            {
                return base.SelectTemplate(item, container);
            }

            if (!(container is FrameworkElement element))
            {
                return base.SelectTemplate(item, container);
            }
            
            var template = (DataTemplate)element.FindResource(visualBlockTemplate.TemplateName);

            visualBlockTemplate.DataContext.InitializeCommand.Execute(visualBlockTemplate.TabContentId);

            return template;
        }
    }
}
