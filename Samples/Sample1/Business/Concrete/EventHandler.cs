using Core.EventManager.Configuration.Attributes;

namespace Sample1.Business.Concrete
{
    public class EventHandler
    {
        [StringMatchListener("SomeEvent")]
        public void HandleEvent()
        {
            //Do some stuff
        }
    }
}
