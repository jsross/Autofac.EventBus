using Core.EventManager.Configuration.Attributes;
using Sample2.Models;

namespace Sample2.Business.Abstract
{
    public interface IMultiStepTaskNotificationManager
    {
        [RegexListener("^PROCESS_.+")]
        void SendNotification(MultiStepTask multiStepTask, string @event);
    }
}