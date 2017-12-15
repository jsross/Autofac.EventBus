using System;
using System.Reflection;

namespace Autofac.EventBus.Model
{
    public class SubscriberRegistration
    {
        private Func<Event,bool> _matchingFunction;

        public MethodInfo TargetMethod { get; private set; }

        public Type SubscriberType
        {
            get
            {
                return TargetMethod.DeclaringType;
            }
        }

        public SubscriberRegistration(Func<Event,bool> matchingFunction, MethodInfo target)
        {
            _matchingFunction = matchingFunction;
            TargetMethod = target;
        }
        
        public bool IsSubscribed(Event @event)
        {
            var result = _matchingFunction.Invoke(@event);

            return result;
        }
        
    }
}