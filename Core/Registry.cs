using System.Collections.Generic;
using System.Reflection;

namespace Core
{
    public class Registry
    {
        private Dictionary<string, List<MethodInfo>> entries = new Dictionary<string, List<MethodInfo>>();

        public void Register(string eventName, MethodInfo method)
        {
            if (!entries.ContainsKey("eventName"))
            {
                entries[eventName] = new List<MethodInfo>();
            }

            entries[eventName].Add(method);
        }

        public List<MethodInfo> GetListeners(string eventName)
        {
            if (!entries.ContainsKey(eventName))
                return null;

            return entries[eventName];
        }
    }
}
