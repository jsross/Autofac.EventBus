using Autofac.EventManagement.Configuration.Attributes;
using Autofac.EventManagement.Infrastructure.Abstract;
using Autofac.Extras.DynamicProxy;
using Sample2.Business.Abstract;
using Sample2.Models;

namespace Sample2.Business.Concrete
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class MultiStepTaskManager : IMultiStepTaskManager
    {
        private IEventAggregator _eventManager;

        public MultiStepTaskManager(IEventAggregator eventManager)
        {
            _eventManager = eventManager;
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

            _eventManager.Enqueue(EventRefKeys.PROCESS_CREATED, new { multiStepTask = multiStepTask });

            return multiStepTask;
        }

        public void Start(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.InProgress;

            _eventManager.Enqueue(EventRefKeys.PROCESS_STARTED, new { multiStepTask = multiStepTask });
        }

        public void Complete(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.Completed;

            _eventManager.Enqueue(EventRefKeys.PROCESS_COMPLETED, new { multiStepTask = multiStepTask });
        }

        public void Cancel(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.Cancelled;

            _eventManager.Enqueue(EventRefKeys.PROCESS_CANCELLED, new { multiStepTask = multiStepTask });
        }
    }
}
