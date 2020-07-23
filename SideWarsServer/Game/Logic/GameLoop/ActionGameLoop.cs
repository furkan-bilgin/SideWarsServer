using SideWarsServer.Game.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public class ActionGameLoop : IGameLoop
    {
        Action action;

        public ActionGameLoop(Action action)
        {
            this.action = action;
        }

        public void Update(IGameRoom gameRoom)
        {
            action();
        }
    }
}
