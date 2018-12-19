using System.Windows.Controls;
using Autofac;

namespace MakeNotes.Framework.Factories
{
    public class ViewFactory : IViewFactory
    {
        private readonly IComponentContext _componentContext;

        public ViewFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public TView Create<TView>() where TView : UserControl
        {
            return _componentContext.Resolve<TView>();
        }
    }
}
