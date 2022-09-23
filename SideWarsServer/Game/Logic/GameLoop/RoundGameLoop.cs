using SideWars.Shared.Packets;
using SideWarsServer.Game.Room;
using System.Collections.Generic;
using System.Linq;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public class RoundGameLoop : IGameLoop
    {
        public void Update(IGameRoom gameRoom)
        {
            if (gameRoom.RoomState != GameRoomState.Started)
                return;

            var aliveTeams = gameRoom.GetAliveTeams();

            // If one of the teams has no alive player...
            if (aliveTeams.Count() < 2)
            {
                gameRoom.RoomState = GameRoomState.RoundUpdate;
            }
        }
    }
}
