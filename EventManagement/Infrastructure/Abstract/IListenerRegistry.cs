using Autofac.EventManagement.Configuration.Attributes;
using System.Collections.Generic;
using System.Reflection;

namespace Autofac.EventManagement.Infrastructure.Abstract
{
    public interface IListenerRegistry
    {
        void Register(EventListenerAttribute attribute, MethodInfo method);
        List<MethodInfo> GetListeners(string eventName);
    }
}
