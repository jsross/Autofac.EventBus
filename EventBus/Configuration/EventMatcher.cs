using mojr.Autofac.EventBus.Model;

namespace mojr.Autofac.EventBus.Configuration
{
    public interface EventMatcher
    {
        bool Evaluate(Event @event);
    }
}