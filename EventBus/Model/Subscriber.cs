using Autofac.EventBus.Models;
using System.Reflection;
using System;

namespace Autofac.EventManagement.Infrastructure.Model
{
    public class Subscriber
    {
        private object _instance;
        private MethodInfo _target;

        public Subscriber(object instance, MethodInfo target)
        {
            _instance = instance;
            _target = target;
        }

        public void Invoke(Event @event)
        {
            var arguments = MapArguments(@event.EventScope);

            _target.Invoke(_instance, arguments);
        }

        protected object[] MapArguments(ILifetimeScope scope)
        {
            var parameters = _target.GetParameters();

            object[] arguments = null;

            if (parameters.Length > 0)
            {
                arguments = new object[parameters.Length];

                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];
                    var paramaterType = parameter.ParameterType;

                    object instance = null;

                    if (!scope.TryResolve(paramaterType, out instance))
                    {
                        bool canBeNull = !paramaterType.IsValueType || (Nullable.GetUnderlyingType(paramaterType) != null);

                        if (!canBeNull)
                        {
                            throw new Exception("Unable to find value in context for unnullable type");
                        }

                        continue;
                    }

                    arguments[index] = instance;
                }
            }

            return arguments;
        }
        
    }
}
