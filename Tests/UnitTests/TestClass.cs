using NUnit.Framework;
using System.Linq;
using UnitTests.Core;
using Autofac;
using UnitTests.Core.Managers;
using UnitTests.Core.Models;

namespace UnitTests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            var container = AutofacConfig.Configure();

            ILifetimeScope scope = container.BeginLifetimeScope();

            var processManager = scope.Resolve<IProcessManager>();
            var workItemManager = scope.Resolve<IWorkItemManager>();

            var process = processManager.Create();

            Assert.AreEqual(ProcessStatus.Created, process.Status);

            var firstWorkItem = process.WorkItems.First();

            workItemManager.Start(firstWorkItem);

            Assert.AreEqual(ProcessStatus.InProgress, process.Status);
        }
    }
}
