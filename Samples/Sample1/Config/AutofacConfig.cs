using Autofac;
using Autofac.EventManagement.Configuration;
using Autofac.Extras.DynamicProxy;
using Sample1.Business.Abstract;
using Sample1.Business.Concrete;

namespace Sample1.Config
{
    public class AutofacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            //Register the publishing class
            builder.RegisterType<Manager>()
                   .As<IManager>()
                   .EnableInterfaceInterceptors();

            //Register the listening class
            builder.RegisterType<EventHandler>();

            //Register the WorkflowModule with the assembly that contains the listening class
            //This will also register the EventPublisherInterceptor and the IEventHub
            builder.RegisterModule(new WorkflowModule(typeof(EventHandler).Assembly));

            var container = builder.Build();

            return container;
        }
    }
}
