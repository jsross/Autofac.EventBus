using Sample2.Business.Abstract;
using System;
using Autofac.EventBus.Models;

namespace Sample2.Business.Concrete
{
    public class EventLog : IEventLog
    {
        public void LogEvent(Event @event)
        {
            Console.Out.WriteLine(@event.EventName);
        }
    }
}
