using System;
using System.Reflection;

namespace Autofac.EventBus.Models
{
    public class ScopeBackedEvent : Event
    {
        private ILifetimeScope _scope;
         
        public ScopeBackedEvent(string eventName,
                                Event parentEvent = null) : base(eventName, parentEvent) { }

        internal ILifetimeScope EventScope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        public override object[] MapArguments(MethodInfo target)
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
                    
                    if (!_scope.TryResolve(paramaterType, out instance))
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
