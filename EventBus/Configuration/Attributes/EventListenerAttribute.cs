using System;
using Autofac.EventBus.Models;

namespace Autofac.EventBus.Configuration.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class EventListenerAttribute : Attribute, EventMatcher
    {
        public abstract bool Evaluate(Event @event);
    }
}