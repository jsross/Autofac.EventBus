using System;

namespace Autofac.EventBus.Models
{
    public sealed class Event
    {
        private ILifetimeScope _eventScope;
        private readonly string _eventName;
        private readonly Event _parentEvent;

        internal Event(string eventName, Event parentEvent = null)
        {
            if (eventName == null)
                throw new ArgumentNullException("eventName");

            _eventName = eventName;
            _parentEvent = parentEvent;
        }

        internal ILifetimeScope EventScope { get { return _eventScope; } set { _eventScope = value; } }

        public string EventName { get { return _eventName; } }

        public Event ParentEvent { get { return _parentEvent; } }
    }
}
