using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    public interface IWorkItemManager
    {
        void Start(WorkItem workItem);
        void Complete(WorkItem workItem);
        void Cancelled(WorkItem workItem);
    }
}
