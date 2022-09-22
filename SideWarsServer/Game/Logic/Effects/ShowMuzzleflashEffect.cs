using Ara3D;
using SideWars.Shared.Game;
using SideWarsServer.Game.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Logic.Effects
{
    public class ShowMuzzleFlashEffect : IEffect
    {
        public Player player;

        public ShowMuzzleFlashEffect(Player player)
        {
            this.player = player;
        }

        public void Start(IGameRoom room)
        {
            // Need a neet-er way of sending bullets, either client-side prediction or a seperate packet. This just makes things harder to understand.
            room.SpawnParticle(ParticleType.MuzzleFlash, Vector3.Zero, new float[] { player.Id, player.PlayerCombat.CurrentAmmo });
        }
    }
}
