using System;
using System.Reflection;

namespace Autofac.EventBus.Models
{
    public abstract class Event
    {
        private readonly string _eventName;
        private readonly Event _parentEvent;

        internal Event(string eventName, Event parentEvent = null)
        {
            if (eventName == null)
                throw new ArgumentNullException("eventName");

            _eventName = eventName;
            _parentEvent = parentEvent;
        }

        public string EventName { get { return _eventName; } }

        public Event ParentEvent { get { return _parentEvent; } }

        public abstract object[] MapArguments(MethodInfo target);

    }
}
