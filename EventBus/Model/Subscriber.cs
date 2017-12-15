using System.Reflection;

namespace Autofac.EventBus.Infrastructure.Model
{
    public class Subscriber
    {
        private readonly object _instance;
        private readonly MethodInfo _targetMethod;

        public Subscriber(object instance, MethodInfo targetMethod)
        {
            _instance = instance;
            _targetMethod = targetMethod;
        }

        public object Instance
        {
            get
            {
                return _instance;
            }
        }
        
        public MethodInfo TargetMethod {
            get
            {
                return _targetMethod;
            }
        }
    }
}
