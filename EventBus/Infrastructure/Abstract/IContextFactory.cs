using mojr.Autofac.EventBus.Model;

namespace mojr.Autofac.EventBus.Infrastructure.Abstract
{
    public interface IContextFactory
    {
        IContext Create(Event @event, object context);
    }
}
