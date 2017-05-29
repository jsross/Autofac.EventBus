using Autofac.EventManagement.Infrastructure.Abstract;
using Castle.DynamicProxy;

namespace Autofac.EventManagement.Configuration.Attributes
{
    public class EventPublisherInterceptor : IInterceptor
    {
        private IEventBus _eventBus;

        public EventPublisherInterceptor(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            _eventBus.ProcessQueue();
        }
    }
}
