using NUnit.Framework;
using System.Linq;
using Autofac;
using Sample1.Business.Abstract;
using Sample1.Models;

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

            var processManager = scope.Resolve<IMultiStepTaskManager>();
            var workItemManager = scope.Resolve<IWorkItemManager>();

            var multiStepTask = processManager.Create();

            Assert.AreEqual(MultiStepTaskStatus.Created, multiStepTask.Status);

            var firstWorkItem = multiStepTask.WorkItems.First();

            workItemManager.Start(firstWorkItem);

            Assert.AreEqual(MultiStepTaskStatus.InProgress, multiStepTask.Status);
        }
    }
}
