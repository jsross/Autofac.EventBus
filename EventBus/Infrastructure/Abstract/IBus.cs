namespace Autofac.EventBus.Infrastructure.Abstract
{
    public interface IBus
    {
        void Post(string @event, object context = null);

        void ProcessQueue();
    }
}
