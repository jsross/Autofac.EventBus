using mojr.Autofac.EventBus.Configuration.Attributes;
using mojr.Autofac.EventBus.Infrastructure.Abstract;
using mojr.Autofac.EventBus.Infrastructure.Concrete;
using Autofac;

namespace mojr.Autofac.EventBus.Configuration
{
    public class ConfigModule : Module
    {
        private ISubscriberRegistry _listenerRegistry;

        public ConfigModule(params System.Reflection.Assembly[] assemblies)
        {
            _listenerRegistry = ListenerRegistryConfigurator.Configure(assemblies);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventPublisherInterceptor>();

            builder.RegisterType<Bus>()
                   .As<IBus>()
                   .InstancePerLifetimeScope();

            builder.RegisterInstance(_listenerRegistry);

            builder.RegisterType<ScopedBackedContextFactory>()
                   .As<IContextFactory>()
                   .SingleInstance();

            builder.RegisterType<EventFactory>()
                   .As<IEventFactory>()
                   .SingleInstance();
        }
    }
}
