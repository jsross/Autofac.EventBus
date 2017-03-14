using Autofac.Extras.DynamicProxy;
using Core;
using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    [Intercept(typeof(EventPublisher))]
    public class WorkItemManager : IWorkItemManager
    {
        private EventManager _eventPublisher;

        public WorkItemManager(EventManager eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Start(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.InProgress;

            _eventPublisher.Publish(EventRefKeys.WORKITEM_STARTED, workItem);
            //whats missing here is some sort of workflow context, or a 
            //Unit of work context
        }

        public void Complete(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Completed;

            _eventPublisher.Publish(EventRefKeys.WORKITEM_COMPLETED, workItem);
        }

        public void Cancelled(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Cancelled;
        }
    }
}
