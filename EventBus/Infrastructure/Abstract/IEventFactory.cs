using Autofac.EventBus.Model;

namespace Autofac.EventBus.Infrastructure.Abstract
{
    public interface IEventFactory
    {
        Event CreateEvent(string eventName, object context, Event parent = null);
    }
}
