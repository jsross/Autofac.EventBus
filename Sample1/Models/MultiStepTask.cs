using System.Collections.Generic;

namespace Sample1.Models
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
