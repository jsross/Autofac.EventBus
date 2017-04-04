using Core.Attributes;
using Sample1.Business.Abstract;
using Sample1.Models;
using System.Linq;

namespace Sample1.Business.Concrete
{
    public class WorkflowEventHandler : IWorkflowEventHandler
    {
        private IMultiStepTaskManager _processManager;
        private IWorkItemManager _workItemManager;

        public WorkflowEventHandler(IMultiStepTaskManager processManager,
                                    IWorkItemManager workItemManager)
        {
            _processManager = processManager;
            _workItemManager = workItemManager;
        }

        public void HandleWorkItemStarted(WorkItem workItem)
        {
            var multiStepTask = workItem.MultiStepTask;

            if (multiStepTask.Status != MultiStepTaskStatus.InProgress)
            {
                _processManager.Start(workItem.MultiStepTask);
            }
        }

        public void HandleWorkItemCompleted(WorkItem workItem)
        {
            var multiStepTask = workItem.MultiStepTask;

            if (multiStepTask.WorkItems.All(i => i.Status == WorkItemStatus.Completed))
            {
                _processManager.Complete(multiStepTask);
            }
        }

        public void HandleProcessCancelled(MultiStepTask multiStepTask)
        {
            foreach (var workItem in multiStepTask.WorkItems)
            {
                _workItemManager.Cancelled(workItem);
            }
        }
    }
}
