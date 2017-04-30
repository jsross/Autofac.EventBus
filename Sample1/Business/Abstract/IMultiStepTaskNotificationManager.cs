using Core.Attributes;
using Sample1.Models;

namespace Sample1.Business.Abstract
{
    public interface IMultiStepTaskNotificationManager
    {
        [RegexListener("^PROCESS_.+")]
        void SendNotification(MultiStepTask multiStepTask, string @event);
    }
}