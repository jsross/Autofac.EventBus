using Autofac;
using NUnit.Framework;
using Sample1.Business;
using Sample1.Config;

namespace UnitTests
{
    [TestFixture]
    public class Sample1Tests
    {
        [Test]
        public void TestMethod()
        {
            var container = AutofacConfig.Configure();

            ILifetimeScope scope = container.BeginLifetimeScope();

            var manager = scope.Resolve<Manager>();

            manager.DoSomething();

            Assert.IsTrue(EventHandler.SomethingHappened);
        }
    }
}
