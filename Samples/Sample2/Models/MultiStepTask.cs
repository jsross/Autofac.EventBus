using System.Collections.Generic;

namespace Sample2.Models
{
    public class MultiStepTask
    {
        public MultiStepTaskStatus Status { get; set; }

        public List<WorkItem> WorkItems { get; private set; }

        public MultiStepTask()
        {
            WorkItems = new List<WorkItem>();
        }
    }
}
