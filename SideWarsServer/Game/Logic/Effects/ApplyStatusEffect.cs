using Ara3D;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;
using SideWars.Shared.Game;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Utils;
using System;

namespace SideWarsServer.Game.Logic.Effects
{
    public class ApplyStatusEffect : IEffect
    {
        private Entity entity;
        private IStatusEffect statusEffect;

        public ApplyStatusEffect(Entity entity, IStatusEffect statusEffect)
        {
            this.entity = entity;
            this.statusEffect = statusEffect;
        }

        public void Start(IGameRoom room)
        {
            entity.StatusEffects.Add(statusEffect);
            //statusEffect.Start(entity);
            //room.RoomScheduler.ScheduleJobAfter(() => statusEffect.Stop(), Convert.ToInt32(statusEffect.Period * LogicTimer.FramesPerSecond));
        }
    }
}
