using Autofac.EventBus.Models;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

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
            var arguments = MapArguments(@event.Context);

            _target.Invoke(_instance, arguments);
        }

        private object[] MapArguments(Dictionary<string, object> context)
        {
            var parameters = _target.GetParameters();

            object[] arguments = null;

            if (parameters.Any())
            {
                arguments = new object[parameters.Length];

                for(var index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];

                    if (!context.ContainsKey(parameter.Name))
                    {
                        continue;
                    }

                    //TODO (JSR) check type

                    arguments[index] = context[parameter.Name];
                }
            }

            return arguments;
        }
        
    }
}
