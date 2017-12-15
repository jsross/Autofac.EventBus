using System.Linq;
using System.Collections.Concurrent;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Model;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class Bus : IBus
    {
        public static Event CurrentEvent;

        private readonly IEventFactory _eventFactory;
        private readonly ILifetimeScope _currentScope;
        private readonly ISubscriberRegistry _registry;

        private EventQueue _eventQueue;

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

            _eventQueue = new EventQueue(currentScope);
        }

        public void Post(string eventName,
                         object context = null)
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
                EventQueue localQueue = null;

                lock (_lockObject)
                {
                    localQueue = _eventQueue;
                    _eventQueue = new EventQueue(localQueue.Scope);
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

        private void ProcessQueue(EventQueue queue)
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
                var arguments = @event.Context.MapArguments(subscriber.TargetMethod);

                subscriber.TargetMethod.Invoke(subscriber.Instance, arguments);
            }

            CurrentEvent = null;
        }

    }
}