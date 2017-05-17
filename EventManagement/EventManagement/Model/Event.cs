using System.Collections.Generic;

namespace Core.EventManagement.Models
{
    public class Event
    {
        public string EventName { get; set; }

        public Dictionary<string,object> Context { get; set; }
    }
}
