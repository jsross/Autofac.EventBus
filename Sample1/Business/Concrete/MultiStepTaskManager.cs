using Autofac.Extras.DynamicProxy;
using Core;
using Sample1.Business.Abstract;
using Sample1.Models;

namespace Sample1.Business.Concrete
{
    [Intercept(typeof(EventPublisher))]
    public class MultiStepTaskManager : IMultiStepTaskManager
    {
        private EventManager _eventManager;

        public MultiStepTaskManager(EventManager eventManager)
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

            _eventManager.Publish(EventRefKeys.PROCESS_CREATED, new { multiStepTask = multiStepTask });

            return multiStepTask;
        }

        public void Start(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.InProgress;

            _eventManager.Publish(EventRefKeys.PROCESS_STARTED, new { multiStepTask = multiStepTask });
        }

        public void Complete(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.Completed;

            _eventManager.Publish(EventRefKeys.PROCESS_COMPLETED, new { multiStepTask = multiStepTask });
        }

        public void Cancel(MultiStepTask multiStepTask)
        {
            multiStepTask.Status = MultiStepTaskStatus.Cancelled;

            _eventManager.Publish(EventRefKeys.PROCESS_CANCELLED, new { multiStepTask = multiStepTask });
        }
    }
}
