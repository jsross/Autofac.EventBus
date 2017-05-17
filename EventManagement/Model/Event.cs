using System.Collections.Generic;

namespace Autofac.EventManagement.Models
{
    public class Event
    {
        public string EventName { get; set; }

        public Dictionary<string,object> Context { get; set; }
    }
}
