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
            for (int i = 0; i < player.PlayerInfo.BulletsPerShoot; i++)
            {
                new BulletSpawnEffect(player).Start(room);
            }

            new ShowMuzzleFlashEffect(player).Start(room);
        }
    }
}
