using System.Collections.Generic;

namespace UnitTests.Core.Models
{
    public class Process
    {
        public ProcessStatus Status { get; set; }

        public List<WorkItem> WorkItems { get; private set; }

        public Process()
        {
            WorkItems = new List<WorkItem>();
        }
    }
}
