using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Networking;
using System.Collections.Generic;

namespace SideWarsServer.Game.Logic
{
    public class Entity
    {
        public int Id { get; set; }
        public int BirthTick { get; set; }

        public Vector3 Location { get; set; }
        public ICollider Collider { get; set; }
        public IEntityMovement Movement { get; set; }
        public EntityType Type { get; set; }
        public EntityTeam Team { get; set; }
        public EntityInfo EntityInfo { get; set; }

        public int BaseHealth { get; set; }
        public int Health { get; set; } = -1;
        public bool IsImmortal { get; set; }

        public List<IStatusEffect> StatusEffects { get; protected set; }

        public Entity(EntityInfo entityInfo, EntityTeam team)
        {
            Team = team;
            EntityInfo = entityInfo;

            StatusEffects = new List<IStatusEffect>();
            Collider = new SquareCollider(Location, entityInfo.HitBoxMin, entityInfo.HitBoxMax);
        }

        public virtual void UpdateEntityInfo(EntityInfo info)
        {
            BaseHealth = info.BaseHealth;

            if (Health == -1)
                Health = info.BaseHealth;
        }

        public virtual void Hurt(int damage)
        {
            if (IsImmortal)
                return;

            if (damage >= Health)
            {
                Health = 0;
            }
            else
            {
                Health -= damage;
            }
        }

        public virtual void Heal(int hp)
        {
            if (IsImmortal)
                return;

            if (Health + hp >= BaseHealth)
            {
                Health = BaseHealth;
            }
            else
            {
                Health += hp;
            }
        }

        public virtual void Kill()
        {
            Health = 0;
        }

        public virtual void Packetify(ref List<ushort> data, ref List<float> bigData, PlayerConnection connection)
        {
        }
    }
}
