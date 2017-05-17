using Autofac;
using Core.EventManagement.Infrastructure;
using Core.EventManager.Configuration.Attributes;
using System.Reflection;

namespace Core.EventManagement.Configuration
{
    public class WorkflowModule : Autofac.Module
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
