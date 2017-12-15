using Sample2.Business.Abstract;
using Autofac.EventBus.Model;
using System.Diagnostics;

namespace Sample2.Business.Concrete
{
    public class EventLog : IEventLog
    {
        public void LogEvent(Event @event)
        {
            Debug.WriteLine("{0} <-- {1}", @event.EventName, @event.ParentEvent != null ? @event.ParentEvent.EventName : "");
        }
    }
}
