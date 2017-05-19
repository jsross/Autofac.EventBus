using Autofac.EventManagement.Configuration.Attributes;
using Autofac.EventManagement.Infrastructure.Abstract;
using Autofac.Extras.DynamicProxy;

namespace Sample1.Business
{
    [Intercept(typeof(EventPublisherInterceptor))]
    public class Manager
    {
        private IEventAggregator _eventAggregator;

        public Manager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public virtual void DoSomething()
        {
            //Do some stuff

            _eventAggregator.Enqueue("SomeEvent");
        }
    }
}
