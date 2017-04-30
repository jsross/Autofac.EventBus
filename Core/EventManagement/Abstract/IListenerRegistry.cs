using Core.Attributes;
using System.Collections.Generic;
using System.Reflection;

namespace Core.EventManagement.Abstract
{
    public interface IListenerRegistry
    {
        void Register(EventListenerAttribute attribute, MethodInfo method);
        List<MethodInfo> GetListeners(string eventName);
    }
}
