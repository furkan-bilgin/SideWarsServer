using SideWarsServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SideWarsServer.Threading
{
    public class TaskController
    {
        private List<Task> tasks;

        public TaskController(int threadCount)
        {
            tasks = new List<Task>();
            for (int i = 0; i < threadCount; i++)
            {
                var id = i;
                var logicTimer = Server.Instance.LogicController.LogicTimers[i];
                tasks.Add(Task.Run(() => TaskLoop(id, logicTimer))); // Create task and give them their ids
            }
        }

        private void TaskLoop(int id, LogicTimer logicTimer)
        {
            while (true)
            {
                logicTimer.Update();
                Thread.Sleep(20);
            }
        }
    }
}
