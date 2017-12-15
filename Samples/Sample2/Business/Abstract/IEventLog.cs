using Autofac.EventBus.Configuration.Attributes;
using Autofac.EventBus.Model;

namespace Sample2.Business.Abstract
{
    public interface IEventLog
    {
        [RegexListener(".*")]
        void LogEvent(Event @event);
    }
}
