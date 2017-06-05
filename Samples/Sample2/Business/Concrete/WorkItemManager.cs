using Autofac.EventBus.Configuration.Attributes;
using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.Extras.DynamicProxy;
using Sample2.Business.Abstract;
using Sample2.Models;

namespace Sample2.Business.Concrete
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class WorkItemManager : IWorkItemManager
    {
        private IBus _bus;

        public WorkItemManager(IBus bus)
        {
            _bus = bus;
        }

        public void Start(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.InProgress;

            _bus.Post(EventRefKeys.WORKITEM_STARTED, new { workItem });
        }

        public void Complete(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Completed;

            _bus.Post(EventRefKeys.WORKITEM_COMPLETED, new { workItem });
        }

        public void Cancelled(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Cancelled;

            _bus.Post(EventRefKeys.WORKITEM_CANCELLED, new { workItem });
        }
    }
}
