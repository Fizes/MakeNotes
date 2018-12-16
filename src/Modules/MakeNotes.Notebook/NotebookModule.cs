using Autofac;
using MakeNotes.Notebook.Providers;

namespace MakeNotes.Notebook
{
    public class NotebookModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly).PublicOnly();
            builder.RegisterType<NavigationContext>().As<INavigationContext>().SingleInstance();
        }
    }
}
