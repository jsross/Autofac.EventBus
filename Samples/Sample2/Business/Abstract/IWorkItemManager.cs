using Sample2.Models;

namespace Sample2.Business.Abstract
{
    public interface IWorkItemManager
    {
        void Start(WorkItem workItem);

        void Complete(WorkItem workItem);

        void Cancelled(WorkItem workItem);
    }
}