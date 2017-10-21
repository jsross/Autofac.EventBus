using System.Reflection;

namespace Autofac.EventBus.Infrastructure.Abstract
{
    public interface IContext
    {
        object[] MapArguments(MethodInfo target);
    }
}
