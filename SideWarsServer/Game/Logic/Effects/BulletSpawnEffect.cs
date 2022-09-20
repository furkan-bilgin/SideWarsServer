using Ara3D;
using SideWars.Shared.Game;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;
using System;

namespace SideWarsServer.Game.Logic.Effects
{
    public class BulletSpawnEffect : IEffect
    {
        private Player player;

        public BulletSpawnEffect(Player player)
        {
            this.player = player;
        }

        public void Start(IGameRoom room)
        {
            var projectile = room.ProjectileSpawner.SpawnProjectile(player.PlayerInfo.ProjectileType, player);
            if (projectile is GalacticusBullet)
            {
                var galacticusBullet = (GalacticusBullet)projectile;
                galacticusBullet.ChooseTarget(room);
            } 

            room.SpawnEntity(projectile);
        }
    }
}
