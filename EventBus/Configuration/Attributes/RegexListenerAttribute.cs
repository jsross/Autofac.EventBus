using Autofac.EventBus.Models;
using System;
using System.Text.RegularExpressions;

namespace Autofac.EventBus.Configuration.Attributes
{
    public class RegexListenerAttribute : EventListenerAttribute
    {
        private Regex _regex;

        public RegexListenerAttribute(string pattern)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern");
            }

            _regex = new Regex(pattern);
        }

        public override bool Evaluate(Event @event)
        {
            var match = _regex.Match(@event.EventName);

            return match.Success;
        }
    }
}
