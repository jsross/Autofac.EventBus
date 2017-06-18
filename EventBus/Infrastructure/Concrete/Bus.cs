using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Models;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class Bus : IBus
    {
        private const string EVENT_CONTEXT_KEY = "event";

        public static Event CurrentEvent;

        private ConcurrentQueue<Event> _eventQueue;

        private ISubscriberRegistry _registry;
        private ILifetimeScope _scope;

        private object _lockObject;
        private bool _inProcess = false;

        public Bus(ISubscriberRegistry registry, ILifetimeScope scope)
        {
            _lockObject = new object();

            _registry = registry;
            _scope = scope;

            _eventQueue = new ConcurrentQueue<Event>();
        }
         
        public void Post(string eventName, object context = null)
        {
            Dictionary<string, object> dictionary = MapContext(context);

            dictionary[EVENT_CONTEXT_KEY] = eventName;

            var entry = new Event(eventName, dictionary, CurrentEvent);

            lock (_lockObject)
            {
                _eventQueue.Enqueue(entry);
            }
        }

        public void ProcessQueue()
        {
            ConcurrentQueue<Event> localQueue = null;
             
            lock (_lockObject)
            {
                if (_inProcess)
                    return;

                _inProcess = true;

                localQueue = _eventQueue;
                _eventQueue = new ConcurrentQueue<Event>();
            }

            Event @event;

            while (localQueue.TryDequeue(out @event))
            {
                ProcessEvent(@event);
            }

            _inProcess = false;
        }

        private void ProcessEvent(Event @event)
        {
            CurrentEvent = @event;

            var subscribers = _registry.GetSubscribers(@event, _scope);

            foreach(var subscriber in subscribers)
            {
                subscriber.Invoke(@event);
            }

            CurrentEvent = null;
        }

        private Dictionary<string, object> MapContext(object objectContext)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            if (objectContext != null)
            {
                var type = objectContext.GetType();

                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var propertyName = property.Name;

                    if (propertyName == EVENT_CONTEXT_KEY)
                    {
                        throw new Exception("Cannot use property name. Reserved");
                    }

                    var value = property.GetValue(objectContext);

                    dictionary[propertyName] = value;
                }
            }

            return dictionary;
        }

        
    }
}