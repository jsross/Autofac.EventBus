using System.Collections.Concurrent;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Models;
using System.Linq;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class Bus : IBus
    {
        private const string EVENT_CONTEXT_KEY = "event";

        public static Event CurrentEvent;

        private ConcurrentQueue<Event> _eventQueue;

        private ISubscriberRegistry _registry;
        private ILifetimeScope _currentScope;

        private object _lockObject;
        private bool _inProcess = false;

        public Bus(ISubscriberRegistry registry, ILifetimeScope currentScope)
        {
            _lockObject = new object();

            _registry = registry;
            _currentScope = currentScope;

            _eventQueue = new ConcurrentQueue<Event>();
        }

        public void Post(string eventName, object context = null)
        {
            var @event = CreateEvent(eventName, context);

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

        private Event CreateEvent(string eventName, object objectContext)
        {
            var builder = new ContainerBuilder();

            var @event = new Event(eventName, CurrentEvent);

            if (objectContext != null)
            {
                var type = objectContext.GetType();

                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(objectContext);
                    var valueType = value.GetType();

                    builder.RegisterInstance(value).As(valueType);
                }
            }

            builder.RegisterInstance<Event>(@event);
            
            var container = builder.Build();
            var scope = container.BeginLifetimeScope();

            @event.EventScope = scope;

            return @event;
        }
    }
}