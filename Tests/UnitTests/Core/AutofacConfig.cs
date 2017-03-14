using Autofac;
using Autofac.Extras.DynamicProxy;
using Core;
using System.Reflection;
using UnitTests.Core.Managers;

namespace UnitTests.Core
{

    public static class AutofacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ProcessManager>()
                   .As<IProcessManager>()
                   .EnableInterfaceInterceptors();

            builder.RegisterType<WorkItemManager>()
                   .As<IWorkItemManager>()
                   .EnableInterfaceInterceptors();

            builder.RegisterType<WorkflowEventManager>();

            builder.RegisterType<EventPublisher>();
            builder.RegisterType<EventManager>();

            builder.Register((e) =>
            {
                var assembly = Assembly.Load("UnitTests");

                var registry = RegistryConfigurator.Configure(assembly);

                return registry;
            });
            
            var container = builder.Build();

            return container;
        }
    }
}
