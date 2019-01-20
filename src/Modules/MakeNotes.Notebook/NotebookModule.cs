using Autofac;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Providers;
using MakeNotes.Notebook.Templates.VisualBlocks.ViewModels;

namespace MakeNotes.Notebook
{
    public class NotebookModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly).PublicOnly();
            builder.RegisterType<NavigationContext>().As<INavigationContext>().SingleInstance();

            builder.RegisterType<PasswordSheetTemplateViewModel>().Keyed<IVisualBlockViewModel>(VisualBlockTypes.PasswordSheet);
        }
    }
}
