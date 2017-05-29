using Autofac.EventManagement.Configuration.Attributes;
using Autofac.EventManagement.Infrastructure.Abstract;
using Autofac.Extras.DynamicProxy;
using Sample2.Business.Abstract;
using Sample2.Models;

namespace Sample2.Business.Concrete
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class WorkItemManager : IWorkItemManager
    {
        private IEventBus _eventBus;

        public WorkItemManager(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.InProgress;

            _eventBus.Post(EventRefKeys.WORKITEM_STARTED, new { workItem });
        }

        public void Complete(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Completed;

            _eventBus.Post(EventRefKeys.WORKITEM_COMPLETED, new { workItem });
        }

        public void Cancelled(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Cancelled;

            _eventBus.Post(EventRefKeys.WORKITEM_CANCELLED, new { workItem });
        }
    }
}
