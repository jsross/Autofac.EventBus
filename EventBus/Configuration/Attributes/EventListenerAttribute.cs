using mojr.Autofac.EventBus.Model;
using System;

namespace mojr.Autofac.EventBus.Configuration.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class EventListenerAttribute : Attribute, EventMatcher
    {
        public abstract bool Evaluate(Event @event);
    }
}