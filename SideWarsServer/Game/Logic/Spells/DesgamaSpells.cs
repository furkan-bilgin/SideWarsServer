using SideWars.Shared.Game;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Logic.Other;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;

namespace SideWarsServer.Game.Logic.Spells
{
    public class DesgamaSpells : PlayerSpells
    {
        public DesgamaSpells() : base()
        {
            SpellInfo = new DesgamaSpellInfo();
        }

        public override bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell)
        {
            var canCast = base.Cast(gameRoom, player, spell);

            if (canCast)
            {
                var baseGameRoom = (BaseGameRoom)gameRoom;

                if (spell.Type == SpellType.DesgamaMissile)
                {
                    // TODO: This will cause bugs. 
                    var playerMovement = (PlayerMovement)player.Movement;
                    playerMovement.IsHalted = true;

                    // Apply muted status during casting
                    player.StatusEffects.Add(new MutedStatusEffect(GameConstants.DESGAMA_FIRST_SPELL_TIME.SecondsToTicks(), gameRoom.Tick)); 
                    baseGameRoom.RoomScheduler.ScheduleJobAfter(() => playerMovement.IsHalted = false, GameConstants.DESGAMA_FIRST_SPELL_TIME.SecondsToTicks());

                    // Spawn missiles with angles
                    var initialDelay = 0.3f;
                    var j = 0;
                    for (int i = -10; i <= 10; i += 4)
                    {
                        var angle = player.Team == SideWars.Shared.Packets.EntityTeam.Blue ? -i : i;
                        baseGameRoom.RoomScheduler.ScheduleJobAfter(() => baseGameRoom.SpawnEntity(new DesgamaMissile(player, angle)), (initialDelay + 0.05f * j).SecondsToTicks());
                        j += 1;
                    }
                }
                else if (spell.Type == SpellType.DesgamaShield)
                {
                    // Apply muted status during casting
                    player.StatusEffects.Add(new MutedStatusEffect(GameConstants.DESGAMA_SECOND_SPELL_TIME.SecondsToTicks(), gameRoom.Tick)); 

                    baseGameRoom.RoomScheduler.ScheduleJobAfter(() =>
                    {
                        // Spawn a shield entity and a spark particle
                        var shield = baseGameRoom.SpawnEntity(new DesgamaShield(player));
                        baseGameRoom.SpawnParticle(ParticleType.Spark, shield.Location);
                    }, GameConstants.DESGAMA_SECOND_SPELL_TIME.SecondsToTicks()); 
                }
            }

            return canCast;
        }
    }
}
