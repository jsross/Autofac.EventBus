using System.Reflection;
using Autofac.EventManagement.Infrastructure;
using Autofac.EventManagement.Configuration.Attributes;
using Core.EventManagement.Infrastructure;

namespace Autofac.EventManagement.Configuration
{
    public class WorkflowModule : Module
    {
        private Assembly[] _assemblies;
        private IListenerRegistry _listenerRegistry;

        public WorkflowModule(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventPublisherInterceptor>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<EventHub>()
                   .As<IEventHub>()
                   .InstancePerLifetimeScope();

            builder.Register((e) =>
            {
                var registry = ListenerRegistryConfigurator.Configure(_assemblies);

                return registry;
            })
            .SingleInstance();
        }
    }
}
