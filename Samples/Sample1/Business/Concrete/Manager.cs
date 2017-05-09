using Autofac.Extras.DynamicProxy;
using Core.EventManagement.Infrastructure;
using Core.EventManager.Configuration.Attributes;
using Sample1.Business.Abstract;

namespace Sample1.Business.Concrete
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class Manager : IManager
    {
        private IEventHub _eventManager;

        public Manager(IEventHub eventManager)
        {
            _eventManager = eventManager;
        }

        public void DoSomething()
        {
            //Do some stuff

            _eventManager.Enqueue("SomeEvent");
        }
    }
}
