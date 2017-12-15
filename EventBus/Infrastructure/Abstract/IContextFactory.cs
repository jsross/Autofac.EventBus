using Autofac.EventBus.Model;

namespace Autofac.EventBus.Infrastructure.Abstract
{
    public interface IContextFactory
    {
        IContext Create(Event @event, object context);
    }
}
