namespace Autofac.EventManagement.Infrastructure.Abstract
{
    public interface IEventBus
    {
        void Post(string @event, object context = null);

        void ProcessQueue();
    }
}
