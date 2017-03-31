using Core.Attributes;
using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    public interface IProcessNotificationManager
    {
        [RegexListener("^PROCESS_.+")]
        void SendNotification(Process process, string @event);
    }
}
