using System;

namespace Autofac.EventManagement.Configuration.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class EventListenerAttribute : Attribute
    {
        public abstract bool DoesEventNameMatch(string eventName);
    }
}