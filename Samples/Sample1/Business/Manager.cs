using Autofac.Extras.DynamicProxy;
using mojr.Autofac.EventBus.Configuration.Attributes;
using mojr.Autofac.EventBus.Infrastructure.Abstract;

namespace Sample1.Business
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class Manager
    {
        private IBus _bus;

        public Manager(IBus bus)
        {
            _bus = bus;
        }

        public virtual void DoSomething()
        {
            //Do some stuff

            _bus.Post("SomeEvent");
        }
    }
}
