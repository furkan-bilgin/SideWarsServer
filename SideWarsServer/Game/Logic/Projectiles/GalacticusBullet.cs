using Ara3D;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics.Movement;
using SideWarsServer.Game.Room;
using SideWarsServer.Networking;
using System.Collections.Generic;
using System.Linq;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class GalacticusBullet : Bullet
    {
        private Player target;

        public GalacticusBullet(Player shooter) : base(shooter)
        {
            ProjectileInfo = SideWars.Shared.Game.ProjectileInfo.GalacticusBullet;
            Type = EntityType.GalacticusBullet;
            Movement = new GalacticusBulletMovement(shooter.Team, ProjectileInfo.Speed, () => {
                if (target != null)
                    return target.Location;
                return Vector3.Zero;
            });
        }

        public void ChooseTarget(IGameRoom room)
        {
            var enemyTeam = Team == EntityTeam.Blue ? EntityTeam.Red : EntityTeam.Blue;

            // Choose the enemy that is closest to our shooter
            target = room.GetPlayersByTeam(enemyTeam).OrderBy((x) => Shooter.Location.DistanceSquared(x.Location)).FirstOrDefault();
        }

        public override void Packetify(ref List<ushort> data, ref List<float> bigData, PlayerConnection connection)
        {
            if (target != null)
                bigData.Add(target.Id);
        }
    }
}
