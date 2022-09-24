using SideWars.Shared.Packets;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public class RoundGameLoop : IGameLoop
    {
        private bool suddenDeathStarted;

        public void Update(IGameRoom gameRoom)
        {
            if (gameRoom.RoomState != GameRoomState.Started)
                return;

            var baseGameRoom = (BaseGameRoom)gameRoom;
            CheckIfRoundOver(baseGameRoom);
            CheckIfTimeOver(baseGameRoom);
        }

        void CheckIfRoundOver(BaseGameRoom gameRoom)
        {
            var aliveTeams = gameRoom.GetAliveTeams();

            // If one of the teams has no alive player...
            if (aliveTeams.Count() < 2)
            {
                gameRoom.RoomState = GameRoomState.RoundUpdate;
            }
        }

        void CheckIfTimeOver(BaseGameRoom gameRoom)
        {
            if (gameRoom.CurrentRoundTime <= 0 && !suddenDeathStarted)
            {
                var players = gameRoom.GetEntities().Where(x => x is Player);

                // Add everyone a poison effect to be sure that the round will end.
                foreach (var player in players)
                {
                    player.StatusEffects.Add(new PoisonStatusEffect(9999999, 0, 5, 1));
                }

                suddenDeathStarted = true;
            }

            if (gameRoom.CurrentRoundTime > 0)
            {
                suddenDeathStarted = false;
            }
        }
    }
}
