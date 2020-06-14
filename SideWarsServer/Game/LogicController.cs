using SideWarsServer.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game
{
    public class LogicController
    {
        public List<LogicTimer> LogicTimers { get; set; }
        private ConcurrentDictionary<int, List<Action>> logicUpdates;

        public LogicController(int threadCount)
        {
            LogicTimers = new List<LogicTimer>();
            logicUpdates = new ConcurrentDictionary<int, List<Action>>();

            for (int i = 0; i < threadCount; i++)
            {
                LogicTimers.Add(new LogicTimer(() => LogicUpdate(i)));
                logicUpdates.TryAdd(i, new List<Action>());
            }
        }

        public void RegisterLogicUpdate(Action action)
        {
            var sortedAsc = logicUpdates.OrderBy(key => key.Value.Count); // Sort logic update count by asc, meaning get the logic update with least amount of jobs
            sortedAsc.First().Value.Add(action); // And add the action there to balance core usage
        }

        public void UnregisterLogicUpdate(Action action)
        {
            foreach (var item in logicUpdates)
            {
                if (item.Value.Contains(action))
                {
                    item.Value.Remove(action);
                }
            }
        }

        private void LogicUpdate(int id)
        {  
            List<Action> currentList;
            logicUpdates.TryGetValue(id, out currentList);
            
            lock (currentList)
            {
                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].Invoke();
                }
            }
        }
    }
}
