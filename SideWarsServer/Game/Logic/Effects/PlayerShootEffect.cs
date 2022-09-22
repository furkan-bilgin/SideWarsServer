using Ara3D;
using SideWars.Shared.Game;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;
using System.Linq;

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
            void shoot()
            {
                // Return if this player is muted.
                if (player.StatusEffects.OfType<MutedStatusEffect>().Any())
                    return;

                for (int i = 0; i < player.PlayerInfo.BulletsPerShoot; i++)
                {
                    new BulletSpawnEffect(player).Start(room);
                }
            };

            new ShowMuzzleFlashEffect(player).Start(room);

            if (player.PlayerInfo.ShootTime > 0)
                room.RoomScheduler.ScheduleJobAfter(shoot, player.PlayerInfo.ShootTime.SecondsToTicks());
            else
                shoot();
        }
    }
}
