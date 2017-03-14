using System.Linq;
using System.Reflection;

namespace Core
{
    public static class RegistryConfigurator
    {
        public static Registry Configure(Assembly assembly)
        {
            var registry = new Registry();

            var methods = assembly.GetTypes()
                      .SelectMany(t => t.GetMethods())
                      .Where(m => m.GetCustomAttributes(typeof(EventListenerAttribute), false).Length > 0)
                      .ToArray();

            foreach (var method in methods)
            {
                var attrib = method.GetCustomAttribute<EventListenerAttribute>();

                registry.Register(attrib.EventName, method);
            }

            return registry;
        }
    }
}
