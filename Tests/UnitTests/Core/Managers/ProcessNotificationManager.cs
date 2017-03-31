using System;
using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    public class ProcessNotificationManager : IProcessNotificationManager
    {
        private const string NOTIFICATION = "Process Event: {0}";
        
        public void SendNotification(Process process, string @event)
        {
            Console.Out.WriteLine(string.Format(NOTIFICATION, @event));
        }
    }
}
