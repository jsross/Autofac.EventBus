using System;
using System.Reflection;
using Autofac.EventBus.Infrastructure.Abstract;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class ScopeBackedContext : IContext
    {
        private ILifetimeScope _scope;

        public ScopeBackedContext(ILifetimeScope scope)
        {
            _scope = scope;
        }

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
