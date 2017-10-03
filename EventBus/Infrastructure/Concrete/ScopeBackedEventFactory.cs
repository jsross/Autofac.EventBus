using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Models;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class ScopeBackedEventFactory : IEventFactory
    {
        public Event CreateEvent(string eventName, object context, Event parent = null)
        {
            var builder = new ContainerBuilder();

            var @event = new ScopeBackedEvent(eventName, parent);

            if (context != null)
            {
                var type = context.GetType();

                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(context);
                    var valueType = value.GetType();

                    builder.RegisterInstance(value).As(valueType);
                }
            }

            builder.RegisterInstance(@event)
                   .As<Event>();

            var container = builder.Build();
            var scope = container.BeginLifetimeScope();

            @event.EventScope = scope;

            return @event;
        }
    }
}