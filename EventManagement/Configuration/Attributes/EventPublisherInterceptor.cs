using Autofac.EventManagement.Infrastructure.Abstract;
using Castle.DynamicProxy;

namespace Autofac.EventManagement.Configuration.Attributes
{
    public class EventPublisherInterceptor : IInterceptor
    {
        private IEventAggregator _eventManager;

        public EventPublisherInterceptor(IEventAggregator eventManager)
        {
            _eventManager = eventManager;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            _eventManager.ProcessQueue();
        }
    }
}
