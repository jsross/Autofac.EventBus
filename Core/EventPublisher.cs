using Castle.DynamicProxy;

namespace Core
{
    public class EventPublisher : IInterceptor
    {
        private EventManager _eventManager;

        public EventPublisher(EventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            _eventManager.ProcessEvents();
        }
    }
}
