using Sample2.Business.Abstract;
using Sample2.Models;
using System.Linq;
using System;

namespace Sample2.Business.Concrete
{
    public class WorkflowEventHandler : IWorkflowEventHandler
    {
        private IMultiStepTaskManager _multiStepTaskManager;
        private IWorkItemManager _workItemManager;

        public WorkflowEventHandler(IMultiStepTaskManager multiStepTaskManager,
                                    IWorkItemManager workItemManager)
        {
            _multiStepTaskManager = multiStepTaskManager;
            _workItemManager = workItemManager;
        }

        public void HandleWorkItemStarted(WorkItem workItem)
        {
            var multiStepTask = workItem.MultiStepTask;

            if (multiStepTask.Status != MultiStepTaskStatus.InProgress)
            {
                _multiStepTaskManager.Start(workItem.MultiStepTask);
            }
        }

        public void HandleWorkItemCompleted(WorkItem workItem)
        {
            var multiStepTask = workItem.MultiStepTask;

            if (multiStepTask.WorkItems.All(i => i.Status == WorkItemStatus.Completed))
            {
                _multiStepTaskManager.Complete(multiStepTask);
            }
        }

        public void HandleMultiStepTaskCancelled(MultiStepTask multiStepTask)
        {
            foreach (var workItem in multiStepTask.WorkItems)
            {
                _workItemManager.Cancelled(workItem);
            }
        }

        public void HandleMultiStepTaskCompleted(MultiStepTask multiStepTask)
        {
            throw new NotImplementedException();
        }
    }
}
