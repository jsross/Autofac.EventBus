namespace Autofac.EventManagement.Infrastructure.Abstract
{
    public interface IEventAggregator
    {
        void Enqueue(string @event, object context = null);

        void ProcessQueue();
    }
}
