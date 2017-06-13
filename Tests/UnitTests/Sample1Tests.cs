using Autofac;
using NUnit.Framework;
using Sample1.Business;
using Sample1.Config;

namespace UnitTests
{
    [TestFixture]
    public class Sample1Tests
    {
        private ILifetimeScope _scope;

        [SetUp]
        public void Init()
        {
            var container = AutofacConfig.Configure();

            _scope = container.BeginLifetimeScope();
        }

        [Test]
        public void TestMethod()
        {
            var manager = _scope.Resolve<Manager>();

            manager.DoSomething();

            Assert.IsTrue(EventHandler.SomethingHappened);
        }
    }
}
