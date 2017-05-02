using System;

namespace Core.EventManager.Configuration.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class EventListenerAttribute : Attribute
    {
        public abstract bool DoesEventNameMatch(string eventName);
    }
}