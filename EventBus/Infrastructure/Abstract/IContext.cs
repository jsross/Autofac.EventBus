using System.Reflection;

namespace mojr.Autofac.EventBus.Infrastructure.Abstract
{
    public interface IContext
    {
        object[] MapArguments(MethodInfo target);
    }
}
