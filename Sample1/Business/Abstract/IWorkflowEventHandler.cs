using Core.EventManager.Configuration.Attributes;
using Sample1.Models;

namespace Sample1.Business.Abstract
{
    public interface IWorkflowEventHandler
    {
        [StringMatchListener(EventRefKeys.WORKITEM_STARTED)]
        void HandleWorkItemStarted(WorkItem workItem);

        [StringMatchListener(EventRefKeys.WORKITEM_COMPLETED)]
        void HandleWorkItemCompleted(WorkItem workItem);

        [StringMatchListener(EventRefKeys.PROCESS_CANCELLED)]
        void HandleProcessCancelled(MultiStepTask multiStepTask);
    }
}
