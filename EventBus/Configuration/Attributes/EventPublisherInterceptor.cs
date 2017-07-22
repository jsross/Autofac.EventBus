using Autofac.EventBus.Infrastructure.Abstract;
using Castle.DynamicProxy;

namespace Autofac.EventBus.Configuration.Attributes
{
    public class EventPublisherInterceptor : IInterceptor
    {
        private IBus _eventBus;

        public EventPublisherInterceptor(IBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            _eventBus.Publish();
        }
    }
}
