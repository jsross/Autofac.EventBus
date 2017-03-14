using Autofac;
using System.Collections.Generic;

namespace Core
{
    public class EventManager
    {
        private Queue<PublishedEntry> _entries;

        private Registry _registry;
        private ILifetimeScope _scope;

        public EventManager(Registry registry, ILifetimeScope scope)
        {
            _registry = registry;
            _scope = scope;

            _entries = new Queue<PublishedEntry>();
        }
         
        public void Publish(string eventName, params object[] arguments)
        {
            var entry = new PublishedEntry
            {
                EventName = eventName,
                EventArguments = arguments
            };
            
            _entries.Enqueue(entry);
        }

        public void ProcessEvents()
        {
            while(_entries.Count > 0)
            {
                var entry = _entries.Dequeue();

                ProcessEvent(entry);
            }
        }

        private void ProcessEvent(PublishedEntry entry)
        {
            var listeners = _registry.GetListeners(entry.EventName);

            foreach(var listener in listeners)
            {
                //TODO (JSR) Find way to map entity objects to invoke params using method arguments
                //var methodArguments = listener.GetGenericArguments();
                //var parameters = new List<object>();

                //foreach(var methodArgument in methodArguments)
                //{

                //}

                var instance = _scope.Resolve(listener.DeclaringType);
                listener.Invoke(instance, entry.EventArguments);
            }
        }
    }
}
