using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Caching;
using Autofac.EventBus.Configuration.Attributes;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventManagement.Model;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    public class ListenerRegistry : IListenerRegistry
    {
        private List<Listener> _listeners;
        private MemoryCache _cache;

        public ListenerRegistry()
        {
            _listeners = new List<Listener>();
            _cache = new MemoryCache("RegistryCache");
        }

        public void Register(EventListenerAttribute attribute, MethodInfo method)
        {
            var listener = new Listener(attribute.DoesEventNameMatch, method);

            _listeners.Add(listener);
        }

        public List<Listener> GetListeners(string eventName)
        {
            lock(_cache)
            {
                if (!_cache.Contains(eventName))
                {
                    var listeners = new List<Listener>();

                    foreach (var listener in _listeners)
                    {
                        if (listener.DoesItMatch(eventName))
                        {
                            listeners.Add(listener);
                        }
                    }

                    var policy = new CacheItemPolicy();

                    _cache.Add(eventName, listeners, policy);
                }
            }

            var result = _cache.Get(eventName) as List<Listener>;

            return result;
        }
    }
}
