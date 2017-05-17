using Castle.DynamicProxy;
using Core.EventManagement.Infrastructure;

namespace Autofac.EventManagement.Configuration.Attributes
{
    public class EventPublisherInterceptor : IInterceptor
    {
        private IEventHub _eventManager;

        public EventPublisherInterceptor(IEventHub eventManager)
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
