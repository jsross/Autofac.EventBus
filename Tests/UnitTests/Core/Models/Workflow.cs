using System.Collections.Generic;

namespace UnitTests.Core.Models
{
    public class Workflow
    {
        public WorkflowStatus Status { get; set; }
        public List<WorkItem> WorkItems { get; private set; }

        public Workflow()
        {
            WorkItems = new List<WorkItem>();
        }
    }
}
