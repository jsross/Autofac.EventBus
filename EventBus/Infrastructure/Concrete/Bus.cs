using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class Bus : IBus
    {
        private const string EVENT_CONTEXT_KEY = "event";

        private Queue<Event> _entries;

        private IListenerRegistry _registry;
        private ILifetimeScope _scope;

        private bool _inProcess = false;

        public Bus(IListenerRegistry registry, ILifetimeScope scope)
        {
            _registry = registry;
            _scope = scope;

            _entries = new Queue<Event>();
        }
         
        public void Post(string @event, object context = null)
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

            var entry = new Event
            {
                EventName = @event,
                Context = dictionary
            };
            
            _entries.Enqueue(entry);
        }

        public void ProcessQueue()
        {
            while(_entries.Count > 0)
            {
                var entry = _entries.Dequeue();

                ProcessEvent(entry);
            }
        }

        private void ProcessEvent(Event entry)
        {
            if (_inProcess)
                return;

            _inProcess = true;

            var listeners = _registry.GetListeners(entry.EventName);
            var context = entry.Context;

            foreach(var listener in listeners)
            {
                object instance = null;

                var resolved = _scope.TryResolve(listener.DeclaringType, out instance);

                if (!resolved)
                    continue;

                var arguments = MapArguments(listener, context);

                listener.Invoke(instance, arguments);
            }

            _inProcess = false;
        }

        private object[] MapArguments(MethodInfo listener, Dictionary<string, object> context)
        {
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

            return arguments;
        }
    }
}
