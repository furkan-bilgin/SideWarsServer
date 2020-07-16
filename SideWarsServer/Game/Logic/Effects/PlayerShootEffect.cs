using Ara3D;
using SideWars.Shared.Game;
using SideWarsServer.Game.Room;
using System;

namespace SideWarsServer.Game.Logic.Effects
{
    public class PlayerShootEffect : IEffect
    {
        private Player player;

        public PlayerShootEffect(Player player)
        {
            this.player = player;
        }

        public void Start(IGameRoom room)
        {
            var projectile = room.ProjectileSpawner.SpawnProjectile(player.PlayerInfo.ProjectileType, player);
            room.SpawnEntity(projectile);
            room.SpawnParticle(ParticleType.MuzzleFlash, Vector3.Zero, new float[] { player.Id });
        }
    }
}
