using Autofac.Extras.DynamicProxy;
using Core;
using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    [Intercept(typeof(EventPublisher))]
    public class WorkItemManager : IWorkItemManager
    {
        private EventManager _eventManager;

        public WorkItemManager(EventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public void Start(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.InProgress;

            _eventManager.Publish(EventRefKeys.WORKITEM_STARTED, workItem);
            //whats missing here is some sort of workflow context, or a 
            //Unit of work context
        }

        public void Complete(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Completed;

            _eventManager.Publish(EventRefKeys.WORKITEM_COMPLETED, workItem);
        }

        public void Cancelled(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Cancelled;
        }
    }
}
