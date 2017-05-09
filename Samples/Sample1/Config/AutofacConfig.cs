using Autofac;
using Autofac.Extras.DynamicProxy;
using Core.EventManagement.Configuration;
using Sample1.Business.Abstract;
using Sample1.Business.Concrete;
using System.Reflection;

namespace Sample1.Config
{
    public class AutofacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Manager>()
                   .As<IManager>()
                   .InstancePerLifetimeScope()
                   .EnableInterfaceInterceptors();

            builder.RegisterType<EventHandler>()
                   .InstancePerLifetimeScope();

            builder.RegisterModule(new WorkflowModule(Assembly.Load("Sample1")));

            var container = builder.Build();

            return container;
        }
    }
}
