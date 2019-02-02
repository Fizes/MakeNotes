using Autofac;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.Common.Infrastructure.Extensions;
using MakeNotes.DAL.Decorators;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Core.Notifications;
using MakeNotes.Notebook.Core.Notifications.Handlers;
using MakeNotes.Notebook.Providers;
using MakeNotes.Notebook.Templates.VisualBlocks.ViewModels;

namespace MakeNotes.Notebook
{
    public class NotebookModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).PublicOnly();
            builder.RegisterType<NavigationContext>().As<INavigationContext>().SingleInstance();

            builder.RegisterType<PasswordSheetTemplateViewModel>().Keyed<IVisualBlockViewModel>(VisualBlockTypes.PasswordSheet);

            builder.RegisterGenericDecorator<TabNotificationHandler, INotificationHandler<TabDeleted>>(typeof(TransactionNotificationHandlerDecorator<>));
        }
    }
}
