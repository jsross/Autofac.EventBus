using System.Collections.Generic;

namespace Autofac.EventBus.Models
{
    public class Event
    {
        public string EventName { get; set; }

        public Dictionary<string,object> Context { get; set; }
    }
}
