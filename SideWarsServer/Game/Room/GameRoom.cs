using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Room
{
    public class GameRoom
    {
        public GameRoom()
        {
            Server.Instance.LogicController.RegisterLogicUpdate(Update);
        }

        ~GameRoom()
        {
            Server.Instance.LogicController.UnregisterLogicUpdate(Update);
        }

        public void Update()
        {
            // TODO: Game logic
        }
    }
}
