using System;
using System.Reflection;

namespace Autofac.EventManagement.Model
{
    public class Listener
    {
        private Func<string,bool> _matchingFunction;

        public MethodInfo TargetMethod { get; private set; }

        public Listener(Func<string,bool> matchingFunction, MethodInfo target)
        {
            _matchingFunction = matchingFunction;
            TargetMethod = target;
        }
        
        public bool DoesItMatch(string value)
        {
            var result = _matchingFunction.Invoke(value);

            return result;
        }

    }
}
