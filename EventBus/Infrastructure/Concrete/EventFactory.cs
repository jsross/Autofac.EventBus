using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Model;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class EventFactory : IEventFactory
    {
        private IContextFactory _contextFactory;

        public EventFactory(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Event CreateEvent(string eventName, object context, Event parent = null)
        {
            var builder = new ContainerBuilder();

            var @event = new Event(eventName, parent);

            @event.Context = _contextFactory.Create(@event, context);

            return @event;
        }
    }
}