using Autofac.Extras.DynamicProxy;
using Core;
using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    [Intercept(typeof(EventPublisher))]
    public class ProcessManager : IProcessManager
    {
        private EventManager _eventManager;

        public ProcessManager(EventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public Process Create()
        {
            var process = new Process
            {
                Status = ProcessStatus.Created
            };

            process.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created, Process = process });
            process.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created, Process = process });
            process.WorkItems.Add(new WorkItem { Status = WorkItemStatus.Created, Process = process });

            _eventManager.Publish(EventRefKeys.PROCESS_CREATED, new { process = process });

            return process;
        }

        public void Start(Process process)
        {
            process.Status = ProcessStatus.InProgress;

            _eventManager.Publish(EventRefKeys.PROCESS_STARTED, new { process = process });
        }

        public void Complete(Process process)
        {
            process.Status = ProcessStatus.Completed;

            _eventManager.Publish(EventRefKeys.PROCESS_COMPLETED, new { process = process });
        }

        public void Cancel(Process process)
        {
            process.Status = ProcessStatus.Cancelled;

            _eventManager.Publish(EventRefKeys.PROCESS_CANCELLED, new { process = process });
        }
    }
}
