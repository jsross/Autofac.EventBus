using System.Collections.Concurrent;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Models;
using System.Linq;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class Bus : IBus
    {
        public static Event CurrentEvent;

        private readonly IEventFactory _eventFactory;
        private readonly ILifetimeScope _currentScope;
        private readonly ISubscriberRegistry _registry;

        private ConcurrentQueue<Event> _eventQueue;

        private object _lockObject;
        private bool _inProcess = false;

        public Bus(IEventFactory eventFactory,
                   ILifetimeScope currentScope,
                   ISubscriberRegistry registry)
        {
            _eventFactory = eventFactory;
            _currentScope = currentScope;
            _registry = registry;

            _lockObject = new object();

            _eventQueue = new ConcurrentQueue<Event>();
        }

        public void Post(string eventName, object context = null)
        {
            var @event = _eventFactory.CreateEvent(eventName, context);

            lock (_lockObject)
            {
                _eventQueue.Enqueue(@event);
            }
        }

        public void Publish()
        {
            lock (_lockObject)
            {
                if (_inProcess)
                    return;

                _inProcess = true;
            }

            do
            {
                ConcurrentQueue<Event> localQueue = null;

                lock (_lockObject)
                {
                    localQueue = _eventQueue;
                    _eventQueue = new ConcurrentQueue<Event>();
                }

                if (!localQueue.Any())
                {
                    break;
                }

                ProcessQueue(localQueue);
            } while (true);

            lock (_lockObject)
            {
                _inProcess = false;
            }
        }

        private void ProcessQueue(ConcurrentQueue<Event> queue)
        {
            Event @event;

            while (queue.TryDequeue(out @event))
            {
                ProcessEvent(@event);
            }
        }

        private void ProcessEvent(Event @event)
        {
            CurrentEvent = @event;

            var subscribers = _registry.GetSubscribers(@event, _currentScope);

            foreach(var subscriber in subscribers)
            {
                subscriber.Invoke(@event);
            }

            CurrentEvent = null;
        }

    }
}