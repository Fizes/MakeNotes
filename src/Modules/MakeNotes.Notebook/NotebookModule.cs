using System;
using Autofac;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.DAL.Decorators;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Core.Notifications;
using MakeNotes.Notebook.Templates.VisualBlocks.ViewModels;

namespace MakeNotes.Notebook
{
    public class NotebookModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).PublicOnly();

            builder.RegisterType<PasswordSheetTemplateViewModel>().Named<IVisualBlockViewModel>(VisualBlockTypes.PasswordSheet);
            builder.Register<Func<string, IVisualBlockViewModel>>(c => {
                var context = c.Resolve<IComponentContext>();
                return templateName => context.ResolveNamed<IVisualBlockViewModel>(templateName);
            });

            builder.RegisterGenericDecorator<INotificationHandler<TabDeleted>>(typeof(TransactionNotificationHandlerDecorator<>));
            builder.RegisterGenericDecorator<INotificationHandler<TabContentDeleted>>(typeof(TransactionNotificationHandlerDecorator<>));
        }
    }
}
