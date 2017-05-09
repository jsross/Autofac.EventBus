using Sample1.Models;

namespace Sample1.Business.Abstract
{
    public interface IWorkItemManager
    {
        void Start(WorkItem workItem);

        void Complete(WorkItem workItem);

        void Cancelled(WorkItem workItem);
    }
}