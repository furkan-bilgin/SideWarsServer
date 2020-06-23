using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;

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
        public int Health { get; set; }
        public bool IsImmortal { get; set; }

        public Entity(EntityInfo entityInfo)
        {
            Health = entityInfo.BaseHealth;
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
            if (Health + hp >= BaseHealth)
                Health = BaseHealth;

            Health += hp;
        }

        public virtual void Kill()
        {
            Health = 0;
        }
    }
}
