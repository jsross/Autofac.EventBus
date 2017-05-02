namespace Core.EventManagement.Infrastructure
{
    public interface IEventHub
    {
        void Enqueue(string @event, object context = null);
        void ProcessQueue();
    }
}
