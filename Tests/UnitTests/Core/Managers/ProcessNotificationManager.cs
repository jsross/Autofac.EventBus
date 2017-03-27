using Core.Attributes;
using System;
using UnitTests.Core.Models;

namespace UnitTests.Core.Managers
{
    public class ProcessNotificationManager : IProcessNotificationManager
    {

        
        public void SendNotification(Process process)
        {
            //Having trouble figuring out a way to get the Event name here. How would it be passed into the current method.
            //The basic idea was to not get in the way of the developer. Let the developer define the method params, and not make  
            //Maybe a context static class?
            Console.Out.WriteLine("Event Name:");
        }
    }
}
