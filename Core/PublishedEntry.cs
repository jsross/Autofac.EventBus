using System.Collections.Generic;

namespace Core
{
    public class PublishedEntry
    {
        public string EventName { get; set; }

        public Dictionary<string,object> Context { get; set; }
    }
}
