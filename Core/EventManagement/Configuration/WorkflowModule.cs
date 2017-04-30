using Autofac;
using Core.EventManagement.Abstract;
using Core.EventManagement.Concrete;
using Core.EventManager.Attributes;
using System.Reflection;

namespace Core.EventManagement.Configuration
{
    public class WorkflowModule : Autofac.Module
    {
        private Assembly[] _assemblies;

        public WorkflowModule(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
            var x = ListenerRegistryConfigurator.Configure(assemblies);
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
