using Autofac.EventBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.EventBus.Configuration
{
    public interface EventMatcher
    {
        bool Evaluate(Event @event);
    }
}
