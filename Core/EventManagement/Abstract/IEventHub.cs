namespace Core.EventManagement.Abstract
{
    public interface IEventHub
    {
        void Enqueue(string @event, object context = null);
        void ProcessQueue();
    }
}
