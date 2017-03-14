using System;

namespace Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventListenerAttribute : Attribute
    {
        public string EventName
        {
            get;
            set;
        }

        public EventListenerAttribute(string eventName)
        {
            EventName = eventName;
        }
    }
}