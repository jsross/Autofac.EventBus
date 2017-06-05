using System;
using System.Linq;

namespace Autofac.EventBus.Configuration.Attributes
{
    public class StringMatchListenerAttribute : EventListenerAttribute
    {
        private string[] _toMatch;

        public StringMatchListenerAttribute(params string[] toMatch)
        {
            if (toMatch == null)
            {
                throw new ArgumentNullException("toMatch");
            }
            
            _toMatch = toMatch;
        }

        public override bool DoesEventNameMatch(string eventName)
        {
            return _toMatch.Contains(eventName);
        }
    }
}
