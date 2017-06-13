using Autofac.EventBus.Configuration.Attributes;
using Autofac.EventManagement.Model;
using System.Collections.Generic;
using System.Reflection;

namespace Autofac.EventBus.Infrastructure.Abstract
{
    public interface IListenerRegistry
    {
        void Register(EventListenerAttribute attribute, MethodInfo method);

        List<Listener> GetListeners(string eventName);
    }
}
