using Core.EventManager.Configuration.Attributes;
using System.Collections.Generic;
using System.Reflection;

namespace Core.EventManagement.Infrastructure
{
    public interface IListenerRegistry
    {
        void Register(EventListenerAttribute attribute, MethodInfo method);
        List<MethodInfo> GetListeners(string eventName);
    }
}
