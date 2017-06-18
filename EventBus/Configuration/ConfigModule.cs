using System.Reflection;
using Autofac.EventBus.Configuration.Attributes;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Infrastructure.Concrete;

namespace Autofac.EventBus.Configuration
{
    public class ConfigModule : Module
    {
        private ISubscriberRegistry _listenerRegistry;

        public ConfigModule(params Assembly[] assemblies)
        {
            _listenerRegistry = ListenerRegistryConfigurator.Configure(assemblies);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventPublisherInterceptor>();

            builder.RegisterType<Bus>()
                   .As<IBus>()
                   .InstancePerLifetimeScope();

            builder.Register((e) =>
            {
                return _listenerRegistry;
            })
            .SingleInstance();
        }
    }
}
