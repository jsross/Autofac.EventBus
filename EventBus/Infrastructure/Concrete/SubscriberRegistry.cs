using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Caching;
using Autofac.EventBus.Configuration.Attributes;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Models;
using Autofac.EventManagement.Model;
using Autofac.EventManagement.Infrastructure.Model;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class SubscriberRegistry : ISubscriberRegistry
    {
        private List<SubscriberRegistration> _registrations;
        private MemoryCache _cache;

        public SubscriberRegistry()
        {
            _registrations = new List<SubscriberRegistration>();
            _cache = new MemoryCache("RegistryCache");
        }

        public void Register(EventListenerAttribute attribute, MethodInfo method)
        {
            var listener = new SubscriberRegistration(attribute.Evaluate, method);

            _registrations.Add(listener);
        }

        public List<Subscriber> GetSubscribers(Event @event, ILifetimeScope scope)
        {
            lock(_cache)
            {
                if (!_cache.Contains(@event.EventName))
                {
                    var subscribers = new List<Subscriber>();

                    foreach (var registration in _registrations)
                    {
                        if (!registration.IsSubscribed(@event))
                        {
                            continue;
                        }

                        object instance;

                        var isResolved = scope.TryResolve(registration.SubscriberType, out instance);
                        
                        if (!isResolved)
                        {
                            continue;
                        }

                        var subscriber = new Subscriber(instance, registration.TargetMethod);

                        subscribers.Add(subscriber);
                    }

                    var policy = new CacheItemPolicy(); //TODO (JSR) is the any point in caching? Its not known that the subscriber is using the event name as a key

                    _cache.Add(@event.EventName, subscribers, policy);
                }
            }

            var result = _cache.Get(@event.EventName) as List<Subscriber>;

            return result;
        }
       
    }
}