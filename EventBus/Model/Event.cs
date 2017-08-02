using System;
using System.Reflection;

namespace Autofac.EventBus.Models
{
    public class Event
    {
        private ILifetimeScope _eventScope;
        private readonly string _eventName;
        private readonly Event _parentEvent;

        internal Event(string eventName, Event parentEvent = null)
        {
            if (eventName == null)
                throw new ArgumentNullException("eventName");

            _eventName = eventName;
            _parentEvent = parentEvent;
        }

        internal ILifetimeScope EventScope {
            get { return _eventScope; }
            set { _eventScope = value; }
        }

        public string EventName { get { return _eventName; } }

        public Event ParentEvent { get { return _parentEvent; } }

        public object[] MapArguments(MethodInfo target)
        {
            var parameters = target.GetParameters();

            object[] arguments = null;

            if (parameters.Length > 0)
            {
                arguments = new object[parameters.Length];

                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];
                    var paramaterType = parameter.ParameterType;

                    object instance = null;

                    if (!EventScope.TryResolve(paramaterType, out instance))
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
