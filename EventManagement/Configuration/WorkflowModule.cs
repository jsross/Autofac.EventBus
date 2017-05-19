using System.Reflection;
using Autofac.EventManagement.Configuration.Attributes;
using Autofac.EventManagement.Infrastructure.Abstract;
using Autofac.EventManagement.Infrastructure.Concrete;

namespace Autofac.EventManagement.Configuration
{
    public class WorkflowModule : Module
    {
        private IListenerRegistry _listenerRegistry;

        public WorkflowModule(params Assembly[] assemblies)
        {
            _listenerRegistry = ListenerRegistryConfigurator.Configure(assemblies);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventPublisherInterceptor>();

            builder.RegisterType<EventAggregator>()
                   .As<IEventAggregator>()
                   .InstancePerLifetimeScope();

            builder.Register((e) =>
            {
                return _listenerRegistry;
            })
            .SingleInstance();
        }
    }
}
