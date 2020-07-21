using SideWars.Shared.Game;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;

namespace SideWarsServer.Game.Logic.Spells
{
    public class HyrexSpells : PlayerSpells
    {
        public HyrexSpells() : base()
        {
            SpellInfo = new HyrexSpellInfo();
        }

        public override bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell)
        {
            var canCast = base.Cast(gameRoom, player, spell);

            if (canCast)
            {
                var baseGameRoom = (BaseGameRoom)gameRoom;

                if (spell.Type == SpellType.HyrexSlide)
                {
                    var hyrexMovement = (HyrexMovement)player.Movement;
                    var hyrexCollider = (SquareCollider)player.Collider;

                    player.StatusEffects.Add(new MutedStatusEffect((HyrexMovement.SLIDE_TIME + 0.25f).SecondsToTicks(), gameRoom.Tick)); // Apply muted status effect to Hyrex, so he can't shoot while sliding

                    hyrexMovement.StartSliding();
                    hyrexCollider.IsEnabled = false;

                    baseGameRoom.RoomScheduler.ScheduleJobAfter(() =>
                    {
                        hyrexCollider.IsEnabled = true;
                    }, HyrexMovement.SLIDE_TIME.SecondsToTicks());
                }
                else if (spell.Type == SpellType.HyrexFastFire)
                {
                    var fastFireSeconds = 2f;

                    player.StatusEffects.Add(new HyrexFastFireStatusEffect(fastFireSeconds.SecondsToTicks(), gameRoom.Tick)); 
                }
            }

            return canCast;
        }
    }
}
