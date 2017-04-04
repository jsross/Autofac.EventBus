using Sample1.Business.Abstract;
using Sample1.Models;
using System;

namespace Sample1.Business.Concrete
{
    public class MultiStepTaskNotificationManager : IMultiStepTaskNotificationManager
    {
        private const string NOTIFICATION = "Process Event: {0}";
        
        public void SendNotification(MultiStepTask multiStepTask, string @event)
        {
            Console.Out.WriteLine(string.Format(NOTIFICATION, @event));
        }
    }
}
