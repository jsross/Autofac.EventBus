using Castle.DynamicProxy;
using Core.EventManagement.Abstract;

namespace Core.EventManager.Attributes
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
