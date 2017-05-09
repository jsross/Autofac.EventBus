namespace Sample2.Models
{
    public class WorkItem
    {
        public WorkItemStatus Status
        {
            get; set;
        }

        public MultiStepTask MultiStepTask
        {
            get; set;
        }
    }
}