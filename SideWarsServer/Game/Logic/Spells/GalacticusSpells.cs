using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Logic.Other;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;
using System.Linq;

namespace SideWarsServer.Game.Logic.Spells
{
    public class GalacticusSpells : PlayerSpells
    {
        public GalacticusSpells() : base()
        {
            SpellInfo = new GalacticusSpellInfo();
        }

        public override bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell)
        {
            var canCast = base.Cast(gameRoom, player, spell);

            if (canCast)
            {
                var baseGameRoom = (BaseGameRoom)gameRoom;

                // Get the closest enemy
                var enemyTeam = player.Team == EntityTeam.Blue ? EntityTeam.Red : EntityTeam.Blue;
                var targetEnemy = baseGameRoom.GetPlayersByTeam(enemyTeam).OrderBy((x) => player.Location.DistanceSquared(x.Location)).FirstOrDefault();

                if (targetEnemy != null)
                {
                    if (spell.Type == SpellType.GalacticusSlow)
                    {
                        // Apply slow to the enemy
                        targetEnemy.StatusEffects.Add(new SlowdownStatusEffect(GameConstants.GALACTICUS_FIRST_SPELL_TIME.SecondsToTicks(), gameRoom.Tick, slowdownPercentage: 50));
                        // Apply poison to the enemy
                        targetEnemy.StatusEffects.Add(new PoisonStatusEffect(GameConstants.GALACTICUS_FIRST_SPELL_TIME.SecondsToTicks(), gameRoom.Tick, damagePerUnitTime: 5, timeInSeconds: 0.4f));

                        baseGameRoom.SpawnParticle(ParticleType.GalacticusSlowSpark, targetEnemy.Location);
                    }
                    else if (spell.Type == SpellType.GalacticusStun)
                    {
                        // Apply stun to the enemy
                        targetEnemy.StatusEffects.Add(new StunStatusEffect(GameConstants.GALACTICUS_SECOND_SPELL_TIME.SecondsToTicks(), gameRoom.Tick));
                        baseGameRoom.SpawnParticle(ParticleType.GalacticusStunSpark, targetEnemy.Location);
                    }
                } 
            
            }

            return canCast;
        }
    }
}
