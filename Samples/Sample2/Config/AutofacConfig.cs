using Autofac;
using Autofac.EventBus.Configuration;
using Autofac.Extras.DynamicProxy;
using Sample2.Business.Abstract;
using Sample2.Business.Concrete;
using System.Reflection;

namespace Sample2.Config
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

            builder.RegisterType<EventLog>()
                   .As<IEventLog>()
                   .InstancePerLifetimeScope();

            builder.RegisterModule(new ConfigModule(Assembly.Load("Sample2")));

            var container = builder.Build();

            return container;
        }
    }
}
