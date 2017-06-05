using Autofac.EventBus.Configuration.Attributes;
using System.Collections.Generic;
using System.Reflection;

namespace Autofac.EventBus.Infrastructure.Abstract
{
    public interface IListenerRegistry
    {
        void Register(EventListenerAttribute attribute, MethodInfo method);
        List<MethodInfo> GetListeners(string eventName);
    }
}
