using Autofac;
using System.Collections.Concurrent;

namespace mojr.Autofac.EventBus.Model
{
    public class EventQueue : ConcurrentQueue<Event>
    {
        private ILifetimeScope _scope;

        public EventQueue(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public ILifetimeScope Scope
        {
            get
            {
                return _scope;
            }
        }

        public EventQueueState State { get; internal set; }

        public enum EventQueueState
        {
            InProcess,
            Completed
        }
    }
}
