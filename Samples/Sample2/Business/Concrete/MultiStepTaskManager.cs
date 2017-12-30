using Autofac.Extras.DynamicProxy;
using mojr.Autofac.EventBus.Configuration.Attributes;
using mojr.Autofac.EventBus.Infrastructure.Abstract;
using Sample2.Business.Abstract;
using Sample2.Models;

namespace Sample2.Business.Concrete
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class MultiStepTaskManager : IMultiStepTaskManager
    {
        private IBus _bus;

        public MultiStepTaskManager(IBus bus)
        {
            _bus = bus;
        }

        public MultiStepTask Create()
        {
            var multiStepTask = new MultiStepTask
            {
                Status = MultiStepTaskStatus.Created
            };

            multiStepTask.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created, MultiStepTask = multiStepTask });
            multiStepTask.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created, MultiStepTask = multiStepTask });
            multiStepTask.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created, MultiStepTask = multiStepTask });

            _bus.Post(EventRefKeys.PROCESS_CREATED, new { multiStepTask = multiStepTask });

            return multiStepTask;
        }

        public void Start(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.InProgress;

            _bus.Post(EventRefKeys.PROCESS_STARTED, new { multiStepTask = multiStepTask });
        }

        public void Complete(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.Completed;

            _bus.Post(EventRefKeys.PROCESS_COMPLETED, new { multiStepTask = multiStepTask });
        }

        public void Cancel(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.Cancelled;

            _bus.Post(EventRefKeys.PROCESS_CANCELLED, new { multiStepTask = multiStepTask });
        }
    }
}
