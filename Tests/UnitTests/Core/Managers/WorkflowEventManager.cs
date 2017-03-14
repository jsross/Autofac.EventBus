using Core;
using System.Linq;
using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    public class WorkflowEventManager
    {
        private IProcessManager _processManager;
        private IWorkItemManager _workItemManager;

        public WorkflowEventManager(IProcessManager processManager,
                                    IWorkItemManager workItemManager)
        {
            _processManager = processManager;
            _workItemManager = workItemManager;
        }

        [EventListenerAttribute(EventRefKeys.WORKITEM_STARTED)]
        public void HandleWorkItemStarted(WorkItem workItem)
        {
            var process = workItem.Process;

            if (process.Status != ProcessStatus.InProgress)
            {
                _processManager.Start(workItem.Process);
            }
        }

        [EventListenerAttribute(EventRefKeys.WORKITEM_COMPLETED)]
        public void HandleWorkItemCompleted(WorkItem workItem)
        {
            var process = workItem.Process;

            if (process.WorkItems.All(i => i.Status == WorkItemStatus.Completed))
            {
                _processManager.Complete(process);
            }
        }

        [EventListenerAttribute(EventRefKeys.PROCESS_CANCELLED)]
        public void HandleProcessCancelled(Process process)
        {
            foreach (var workItem in process.WorkItems)
            {
                _workItemManager.Cancelled(workItem);
            }
        }
    }
}
