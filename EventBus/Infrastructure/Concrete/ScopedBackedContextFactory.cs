using Autofac.EventBus.Infrastructure.Abstract;
using Autofac.EventBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.EventBus.Infrastructure.Concrete
{
    class ScopedBackedContextFactory : IContextFactory
    {
        public IContext Create(Event @event, object context)
        {
            var builder = new ContainerBuilder();

            if (context != null)
            {
                var type = context.GetType();

                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(context);
                    var valueType = value.GetType();

                    builder.RegisterInstance(value).As(valueType);
                }
            }

            builder.RegisterInstance(@event)
                   .As<Event>();

            var container = builder.Build();
            var scope = container.BeginLifetimeScope();

            return new ScopeBackedContext(scope);
        }
    }
}
