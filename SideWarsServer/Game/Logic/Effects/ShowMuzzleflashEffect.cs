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
            room.SpawnParticle(ParticleType.MuzzleFlash, Vector3.Zero, new float[] { player.Id });
        }
    }
}
