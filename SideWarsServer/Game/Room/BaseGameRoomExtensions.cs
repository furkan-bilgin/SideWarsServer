using Ara3D;
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

        public static Player GetPlayer(this IGameRoom room, string token)
        {
            return (Player)room.GetEntities().Where(x => x is Player && ((Player)x).PlayerConnection.Token.Id == token).FirstOrDefault();
        }
    }
}
