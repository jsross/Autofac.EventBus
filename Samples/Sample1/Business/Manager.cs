using Autofac.EventManagement.Configuration.Attributes;
using Autofac.EventManagement.Infrastructure.Abstract;
using Autofac.Extras.DynamicProxy;

namespace Sample1.Business
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class Manager
    {
        private IEventBus _eventBus;

        public Manager(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public virtual void DoSomething()
        {
            //Do some stuff

            _eventBus.Post("SomeEvent");
        }
    }
}
