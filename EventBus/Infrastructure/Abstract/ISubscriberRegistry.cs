using System.Collections.Generic;
using System.Reflection;
using mojr.Autofac.EventBus.Configuration.Attributes;
using mojr.Autofac.EventBus.Model;
using mojr.Autofac.EventBus.Infrastructure.Model;
using Autofac;

namespace mojr.Autofac.EventBus.Infrastructure.Abstract
{
    public interface ISubscriberRegistry
    {
        void Register(EventListenerAttribute attribute, MethodInfo method);

        List<Subscriber> GetSubscribers(Event @event, ILifetimeScope scope);
    }
}
