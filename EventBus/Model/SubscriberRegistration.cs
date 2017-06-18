using Autofac.EventBus.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Autofac.EventManagement.Model
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

        public bool Invoke(object instance, Event @event)
        {
            var arguments = MapArguments(@event.Context);

            TargetMethod.Invoke(instance, arguments);

            return true;
        }

        private object[] MapArguments(Dictionary<string, object> context)
        {
            var parameters = TargetMethod.GetParameters();

            object[] arguments = null;

            if (parameters.Length > 0)
            {
                arguments = new object[parameters.Length];

                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];

                    var parameterName = parameter.Name;

                    if (!context.ContainsKey(parameterName))
                    {
                        var paramaterType = parameter.ParameterType;

                        bool canBeNull = !paramaterType.IsValueType || (Nullable.GetUnderlyingType(paramaterType) != null);

                        if (!canBeNull)
                        {
                            throw new Exception("Unable to find value in context for unnullable type");
                        }

                        continue;
                    }

                    arguments[index] = context[parameterName];
                }
            }

            return arguments;
        }
    }
}
