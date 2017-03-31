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

            _eventManager.Publish(EventRefKeys.WORKITEM_STARTED, new { workItem });
        }

        public void Complete(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Completed;

            _eventManager.Publish(EventRefKeys.WORKITEM_COMPLETED, new { workItem });
        }

        public void Cancelled(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Cancelled;

            _eventManager.Publish(EventRefKeys.WORKITEM_CANCELLED, new { workItem });
        }
    }
}
