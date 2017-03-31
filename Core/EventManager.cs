using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core
{
    public class EventManager
    {
        private const string EVENT_CONTEXT_KEY = "event";

        private Queue<PublishedEntry> _entries;

        private Registry _registry;
        private ILifetimeScope _scope;

        public EventManager(Registry registry, ILifetimeScope scope)
        {
            _registry = registry;
            _scope = scope;

            _entries = new Queue<PublishedEntry>();
        }
         
        public void Publish(string @event, object context = null)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            if (context != null)
            {
                var type = context.GetType();

                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var propertyName = property.Name;

                    if(propertyName == EVENT_CONTEXT_KEY)
                    {
                        throw new Exception("Cannot use property name. Reserved");
                    }

                    var value = property.GetValue(context);

                    dictionary[propertyName] = value;
                }
            }

            dictionary[EVENT_CONTEXT_KEY] = @event; 

            var entry = new PublishedEntry
            {
                EventName = @event,
                Context = dictionary
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
            var context = entry.Context;

            foreach(var listener in listeners)
            {
                //TODO (JSR) Find way to map entity objects to invoke params using method arguments
                //var methodArguments = listener.GetGenericArguments();
                //var parameters = new List<object>();

                //foreach(var methodArgument in methodArguments)
                //{

                //}

                var instance = _scope.Resolve(listener.DeclaringType);

                var parameters = listener.GetParameters();

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

                listener.Invoke(instance, arguments);
            }
        }
    }
}
