using Autofac.EventBus.Model;

namespace Autofac.EventBus.Configuration
{
    public interface EventMatcher
    {
        bool Evaluate(Event @event);
    }
}
