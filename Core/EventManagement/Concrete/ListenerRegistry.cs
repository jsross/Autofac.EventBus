using Core.Attributes;
using Core.EventManagement.Abstract;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Caching;

namespace Core.EventManagement.Concrete
{
    public class ListenerRegistry : IListenerRegistry
    {
        private List<Tuple<EventListenerAttribute, MethodInfo>> _entries;
        private MemoryCache _cache;

        public ListenerRegistry()
        {
            _entries = new List<Tuple<EventListenerAttribute, MethodInfo>>();
            _cache = new MemoryCache("RegistryCache");
        }

        public void Register(EventListenerAttribute attribute, MethodInfo method)
        {
            _entries.Add(new Tuple<EventListenerAttribute, MethodInfo>(attribute, method));
        }

        public List<MethodInfo> GetListeners(string eventName)
        {
            lock(_cache)
            {
                if (!_cache.Contains(eventName))
                {
                    List<MethodInfo> listeners = new List<MethodInfo>();

                    foreach (var item in _entries)
                    {
                        var attribute = item.Item1;

                        if (attribute.DoesEventNameMatch(eventName))
                        {
                            listeners.Add(item.Item2);
                        }
                    }

                    var policy = new CacheItemPolicy();

                    _cache.Add(eventName, listeners, policy);
                }
            }

            var result = _cache.Get(eventName) as List<MethodInfo>;

            return result;
        }
    }
}
