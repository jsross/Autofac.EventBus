using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    public interface IProcessManager
    {
        Process Create();
        void Start(Process process);
        void Complete(Process process);
        void Cancel(Process process);
    }
}
