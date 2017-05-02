using Autofac.Extras.DynamicProxy;
using Core.EventManagement.Infrastructure;
using Core.EventManager.Configuration.Attributes;
using Sample1.Business.Abstract;
using Sample1.Models;

namespace Sample1.Business.Concrete
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class MultiStepTaskManager : IMultiStepTaskManager
    {
        private IEventHub _eventManager;

        public MultiStepTaskManager(IEventHub eventManager)
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
