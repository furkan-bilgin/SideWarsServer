using SideWars.Shared.Game;
using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;

namespace SideWarsServer.Game.Logic.Spells
{
    public class MarkSpells : PlayerSpells
    {
        public MarkSpells() : base()
        {
            SpellInfo = new MarkSpellInfo();
        }

        public override bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell)
        {
            var canCast = base.Cast(gameRoom, player, spell);

            if (canCast)
            {
                var baseGameRoom = (BaseGameRoom)gameRoom;

                if (spell.Type == SpellType.MarkGrenade)
                {
                    player.StatusEffects.Add(new MutedStatusEffect(GameConstants.MARK_FIRST_SPELL_TIME.SecondsToTicks(), gameRoom.Tick)); // Apply muted status effect to Mark. (to prevent him to shoot or cast other spells)

                    baseGameRoom.RoomScheduler.ScheduleJobAfter(() =>
                    {
                        var grenade = baseGameRoom.ProjectileSpawner.SpawnProjectile(ProjectileType.Grenade, player);
                        baseGameRoom.SpawnEntity(grenade);
                    }, GameConstants.MARK_FIRST_SPELL_TIME.SecondsToTicks()); 

                }
                else if (spell.Type == SpellType.MarkHeal)
                {
                    player.Heal(25);
                    baseGameRoom.SpawnParticle(ParticleType.MarkHealup, player.Location, new float[] { player.Id }); // Data contains playerID, in the client-side particle will follow player
                }
            }
           
            return canCast;
        }
    }
}
