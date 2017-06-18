using System;
using System.Collections.Generic;

namespace Autofac.EventBus.Models
{
    public class Event
    {
        private readonly Dictionary<string, object> _context;
        private readonly string _eventName;
        private readonly Event _parentEvent;

        public Event(string eventName, Dictionary<string, object> context = null, Event parentEvent = null)
        {
            if (eventName == null)
                throw new ArgumentNullException("eventName");

            _eventName = eventName;
            _context = context;
            _parentEvent = parentEvent;
        }

        public Dictionary<string, object> Context { get { return _context; } }

        public string EventName { get { return _eventName; } }

        public Event ParentEvent { get { return _parentEvent; } }
    }
}
