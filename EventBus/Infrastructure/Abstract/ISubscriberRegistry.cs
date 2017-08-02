using System.Collections.Generic;
using System.Reflection;
using Autofac.EventBus.Configuration.Attributes;
using Autofac.EventBus.Models;
using Autofac.EventBus.Infrastructure.Model;

namespace Autofac.EventBus.Infrastructure.Abstract
{
    public interface ISubscriberRegistry
    {
        void Register(EventListenerAttribute attribute, MethodInfo method);

        List<Subscriber> GetSubscribers(Event @event, ILifetimeScope scope);
    }
}
