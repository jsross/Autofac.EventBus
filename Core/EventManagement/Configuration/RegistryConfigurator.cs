﻿using Core.EventManagement.Infrastructure;
using Core.EventManager.Configuration.Attributes;
using System.Linq;
using System.Reflection;

namespace Core.EventManagement.Configuration
{
    public static class ListenerRegistryConfigurator
    {
        public static IListenerRegistry Configure(params Assembly[] assembies)
        {
            var registry = new ListenerRegistry();

            var methods = assembies.SelectMany(a => a.GetTypes())
                                   .SelectMany(t => t.GetMethods())
                                   .Where(m => m.GetCustomAttributes(typeof(EventListenerAttribute), false).Length > 0)
                                   .ToArray();

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<EventListenerAttribute>();

                registry.Register(attribute, method);
            }

            return registry;
        }
    }
}
