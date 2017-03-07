using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Core.Models
{
    public class WorkItemManager
    {
        private EventPublisher _eventPublisher;

        public WorkItemManager(EventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Begin(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.InProgress;

            _eventPublisher.Publish(WorkItemEvents.STARTED, workItem);
            //whats missing here is some sort of workflow context, or a 
            //Unit of work context
        }

        public void Complete(WorkItem workItem)
        {
            workItem.Status = WorkItemStatus.Completed;
        }
    }
}
