using System.Reflection;
using Autofac.EventBus.Models;

namespace Autofac.EventBus.Infrastructure.Model
{
    public class Subscriber
    {
        private readonly object _instance;
        private readonly MethodInfo _target;

        public Subscriber(object instance, MethodInfo target)
        {
            _instance = instance;
            _target = target;
        }

        public void Invoke(Event @event)
        {
            var arguments = @event.MapArguments(_target);

            _target.Invoke(_instance, arguments);
        }
        
    }
}
