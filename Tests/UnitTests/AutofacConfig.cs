using Autofac;
using Autofac.Extras.DynamicProxy;
using Core;
using Sample1.Business.Abstract;
using Sample1.Business.Concrete;
using System.Reflection;

namespace UnitTests
{
    public class AutofacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MultiStepTaskManager>()
                   .As<IMultiStepTaskManager>()
                   .InstancePerLifetimeScope()
                   .EnableInterfaceInterceptors();

            builder.RegisterType<WorkItemManager>()
                   .As<IWorkItemManager>()
                   .InstancePerLifetimeScope()
                   .EnableInterfaceInterceptors();

            builder.RegisterType<WorkflowEventHandler>()
                   .As<IWorkflowEventHandler>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<MultiStepTaskNotificationManager>()
                   .As<IMultiStepTaskNotificationManager>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<EventPublisher>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<EventManager>()
                   .InstancePerLifetimeScope();

            builder.Register((e) =>
            {
                var assembly = Assembly.Load("Sample1");

                var registry = RegistryConfigurator.Configure(assembly);

                return registry;
            });

            var container = builder.Build();

            return container;
        }
    }
}
