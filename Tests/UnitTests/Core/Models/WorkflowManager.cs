using Core;

namespace UnitTests.Core.Models
{
    public class WorkflowManager
    {
        private EventPublisher _eventPublisher;

        public WorkflowManager(EventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public Workflow Create()
        {
            var workflow = new Workflow
            {
                Status = WorkflowStatus.Created
            };

            workflow.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created });
            workflow.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created });
            workflow.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created });

            return workflow;
        }
    }
}
