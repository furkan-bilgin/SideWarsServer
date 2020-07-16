using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Logic.StatusEffects
{
    public class MutedStatusEffect : IStatusEffect
    {
        public float Period { get; private set; }
        private Entity entity;

        public MutedStatusEffect(float period)
        {
            Period = period;
        }

        public void Start(Entity entity)
        {
            this.entity = entity;

            var player = (Player)entity;
            player.PlayerSpells.Pause();
            player.PlayerCombat.Pause();
        }

        public void Stop()
        {
            var player = (Player)entity;
            player.PlayerSpells.Resume();
            player.PlayerCombat.Resume();
        }
    }
}
