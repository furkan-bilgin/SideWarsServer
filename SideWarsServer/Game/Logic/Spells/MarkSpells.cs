using SideWars.Shared.Game;
using SideWarsServer.Game.Room;
using System;

namespace SideWarsServer.Game.Logic.Spells
{
    public class MarkSpells : IPlayerSpells
    {
        public SpellTimer SpellTimer { get; set; }
        public PlayerSpellInfo SpellInfo { get; set; }

        public MarkSpells()
        {
            SpellInfo = new MarkSpellInfo();
            SpellTimer = new SpellTimer();
        }

        public bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell)
        {
            if (!SpellInfo.Spells.Contains(spell))
                throw new Exception("Mark doesn't have the spell " + spell.Type);

            if (!SpellTimer.CanCast(spell))
                return false;

            var baseGameRoom = (BaseGameRoom)gameRoom;

            if (spell.Type == SpellType.MarkGrenade)
            {
                var grenade = baseGameRoom.ProjectileSpawner.SpawnProjectile(ProjectileType.Grenade, player);
                baseGameRoom.SpawnEntity(grenade);
            }
            else if (spell.Type == SpellType.MarkHeal)
            {
                player.Heal(25);
                baseGameRoom.SpawnParticle(ParticleType.MarkHealup, player.Location, new float[] { player.Id }); // Data contains playerID, in the client-side particle will follow player
            }

            return true;
        }
    }
}
