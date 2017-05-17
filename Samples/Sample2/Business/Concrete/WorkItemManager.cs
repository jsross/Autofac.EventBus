using Autofac.EventManagement.Configuration.Attributes;
using Autofac.Extras.DynamicProxy;
using Core.EventManagement.Infrastructure;
using Sample2.Business.Abstract;
using Sample2.Models;

namespace Sample2.Business.Concrete
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class WorkItemManager : IWorkItemManager
    {
        private IEventHub _eventManager;

        public WorkItemManager(IEventHub eventManager)
        {
            _eventManager = eventManager;
        }

        public void Start(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.InProgress;

            _eventManager.Enqueue(EventRefKeys.WORKITEM_STARTED, new { workItem });
        }

        public void Complete(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Completed;

            _eventManager.Enqueue(EventRefKeys.WORKITEM_COMPLETED, new { workItem });
        }

        public void Cancelled(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Cancelled;

            _eventManager.Enqueue(EventRefKeys.WORKITEM_CANCELLED, new { workItem });
        }
    }
}
