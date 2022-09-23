using Ara3D;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Logic;
using System.Collections.Generic;
using System.Linq;

namespace SideWarsServer.Game.Room
{
    public static class BaseGameRoomExtensions
    {
        public static List<Entity> GetNearEntities(this IGameRoom room, Vector3 location, int distance)
        {
            var distSqr = distance * distance;
            var returnList = new List<Entity>();

            foreach (var item in room.Entities)
            {
                if (item.Value.Location.DistanceSquared(location) <= distSqr)
                {
                    returnList.Add(item.Value);
                }
            }

            return returnList;
        }

        public static Player GetPlayer(this IGameRoom room, int tokenID)
        {
            return (Player)room.GetEntities().Where(x => x is Player && ((Player)x).PlayerConnection.Token.ID == tokenID).FirstOrDefault();
        }

        public static List<Player> GetPlayersByTeam(this IGameRoom room, EntityTeam team)
        {
            return room.GetEntities().Where(x => x is Player && x.Team == team).Cast<Player>().ToList();
        }

        public static List<EntityTeam> GetAliveTeams(this IGameRoom room)
        {
            return room.GetEntities().Where(x => x is Player).GroupBy(x => x.Team).Select(x => x.Key).ToList();
        }
    }
}
