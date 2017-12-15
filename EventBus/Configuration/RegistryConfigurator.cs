using mojr.Autofac.EventBus.Configuration.Attributes;
using mojr.Autofac.EventBus.Infrastructure.Abstract;
using mojr.Autofac.EventBus.Infrastructure.Concrete;
using System.Linq;
using System.Reflection;

namespace mojr.Autofac.EventBus.Configuration
{
    public static class ListenerRegistryConfigurator
    {
        public static ISubscriberRegistry Configure(params Assembly[] assembies)
        {
            var registry = new SubscriberRegistry();

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
