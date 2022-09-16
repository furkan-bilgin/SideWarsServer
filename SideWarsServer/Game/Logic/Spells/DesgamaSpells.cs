using SideWars.Shared.Game;
using SideWarsServer.Game.Logic.Effects;
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

                if (spell.Type == SpellType.MarkGrenade)
                {
                    var spellTime = 1f; // Execute spell after 1 seconds

                    player.StatusEffects.Add(new MutedStatusEffect(spellTime.SecondsToTicks(), gameRoom.Tick)); // Apply muted status effect to Mark. (to prevent him to shoot or cast other spells)

                    baseGameRoom.RoomScheduler.ScheduleJobAfter(() =>
                    {
                        var grenade = baseGameRoom.ProjectileSpawner.SpawnProjectile(ProjectileType.Grenade, player);
                        baseGameRoom.SpawnEntity(grenade);
                    }, Convert.ToInt32(spellTime * LogicTimer.FramesPerSecond));

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
