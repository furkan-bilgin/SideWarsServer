using Microsoft.VisualStudio.TestTools.UnitTesting;
using SideWarsServer;
using SideWarsServer.Game;
using SideWarsServer.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SideWarsServerTests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void TaskAndLogicWorks()
        {
            var threads = 4;
            var logicController = new LogicController(threads);
            var taskController = new TaskController(threads, logicController);

            var works = false;
            logicController.RegisterLogicUpdate(() =>
            {
                works = true;
            });

            Thread.Sleep(200);

            Assert.IsTrue(works);
        }
    }
}
