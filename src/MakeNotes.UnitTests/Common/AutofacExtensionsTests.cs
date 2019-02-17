using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using MakeNotes.Common.Core.Notifications;
using MakeNotes.UnitTests.Common.Fakes;
using Xunit;

namespace MakeNotes.UnitTests.Common
{
    public class AutofacExtensionsTests
    {
        private readonly ContainerBuilder _builder;

        public AutofacExtensionsTests()
        {
            _builder = new ContainerBuilder();
        }

        private void RegisterNotificationHandlers()
        {
            var currentAssembly = GetType().Assembly;
            _builder.RegisterAssemblyTypes(currentAssembly).PublicOnly();
            _builder.RegisterAssemblyTypes(currentAssembly)
                .Where(t => !t.Name.EndsWith("Decorator"))
                .AsClosedTypesOf(typeof(INotificationHandler<>))
                .InstancePerLifetimeScope();
        }

        [Fact]
        public void RegisterGenericDecorator_ShouldDecorateOnlySpecifiedType_WhenTypesAreResolved()
        {
            RegisterNotificationHandlers();

            _builder.RegisterGenericDecorator<INotificationHandler<Test1Notification>>(typeof(TestGenericDecorator<>));

            using (var container = _builder.Build())
            {
                var result1 = container.Resolve<IEnumerable<INotificationHandler<Test1Notification>>>();
                var result2 = container.Resolve<IEnumerable<INotificationHandler<Test2Notification>>>();

                Assert.Single(result1);
                Assert.IsType<TestGenericDecorator<Test1Notification>>(result1.First());

                Assert.Single(result2);
                Assert.IsType<Test2NotificationHandler>(result2.First());
            }
        }

        [Fact]
        public void RegisterGenericDecorator_ShouldThrowException_WhenSingleClassImplementsMultipleGenericTypes()
        {
            RegisterNotificationHandlers();

            _builder.RegisterGenericDecorator<INotificationHandler<Test1MultipleNotification>>(typeof(TestGenericDecorator<>));

            using (var container = _builder.Build())
            {
                Assert.ThrowsAny<Exception>(() => container.Resolve<IEnumerable<INotificationHandler<Test1MultipleNotification>>>());
            }
        }
    }
}
