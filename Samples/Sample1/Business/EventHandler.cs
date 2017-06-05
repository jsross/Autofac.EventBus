using Autofac.EventBus.Configuration.Attributes;

namespace Sample1.Business
{
    public class EventHandler
    {
        public static bool SomethingHappened = false;

        [StringMatchListener("SomeEvent")]
        public void HandleEvent()
        {
            //Do some stuff

            SomethingHappened = true;
        }
    }
}
