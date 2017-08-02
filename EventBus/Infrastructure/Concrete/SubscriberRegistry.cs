using System.Collections.Generic;
using System.Reflection;
using Autofac.EventBus.Configuration.Attributes;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Models;
using Autofac.EventBus.Model;
using Autofac.EventBus.Infrastructure.Model;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class SubscriberRegistry : ISubscriberRegistry
    {
        private List<SubscriberRegistration> _registrations;

        public SubscriberRegistry()
        {
            _registrations = new List<SubscriberRegistration>();
        }

        public void Register(EventListenerAttribute attribute, MethodInfo method)
        {
            var listener = new SubscriberRegistration(attribute.Evaluate, method);

            _registrations.Add(listener);
        }

        public List<Subscriber> GetSubscribers(Event @event, ILifetimeScope currentScope)
        {
            var subscribers = new List<Subscriber>();

            foreach (var registration in _registrations)
            {
                if (!registration.IsSubscribed(@event))
                {
                    continue;
                }

                object instance;

                var isResolved = currentScope.TryResolve(registration.SubscriberType, out instance);
                        
                if (!isResolved)
                {
                    continue;
                }

                var subscriber = new Subscriber(instance, registration.TargetMethod);

                subscribers.Add(subscriber);
            }

            return subscribers;
        }
       
    }
}