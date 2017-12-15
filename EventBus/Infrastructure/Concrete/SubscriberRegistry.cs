using System.Collections.Generic;
using System.Reflection;
using mojr.Autofac.EventBus.Configuration.Attributes;
using mojr.Autofac.EventBus.Infrastructure.Abstract;
using mojr.Autofac.EventBus.Model;
using mojr.Autofac.EventBus.Infrastructure.Model;
using Autofac;

namespace mojr.Autofac.EventBus.Infrastructure.Concrete
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