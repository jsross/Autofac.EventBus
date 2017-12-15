using mojr.Autofac.EventBus.Model;

namespace mojr.Autofac.EventBus.Infrastructure.Abstract
{
    public interface IEventFactory
    {
        Event CreateEvent(string eventName, object context, Event parent = null);
    }
}
