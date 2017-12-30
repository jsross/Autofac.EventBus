using Autofac;
using Autofac.Extras.DynamicProxy;
using mojr.Autofac.EventBus.Configuration;
using Sample1.Business;

namespace Sample1.Config
{
    public class AutofacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            //Register the publishing class
            builder.RegisterType<Manager>()
                   .EnableClassInterceptors();

            //Register the listening class
            builder.RegisterType<EventHandler>();

            //Register the WorkflowModule with the assembly that contains the listening class
            //This will also register the EventPublisherInterceptor and the IEventHub
            builder.RegisterModule(new ConfigModule(typeof(EventHandler).Assembly));

            var container = builder.Build();

            return container;
        }
    }
}
