﻿using Ara3D;
using SideWars.Shared.Game;
using SideWarsServer.Game.Room;

namespace SideWarsServer.Game.Logic.Effects
{
    public class ExplosionEffect : IEffect
    {
        private Vector3 location;
        private int radius;
        private int damage;

        public ExplosionEffect(Vector3 location, int radius, int damage)
        {
            this.location = location;
            this.radius = radius;
            this.damage = damage;
        }

        public void Start(IGameRoom room)
        {
            var entities = room.GetNearEntities(location, radius);
            foreach (var entity in entities)
            {
                entity.Hurt(damage);
                room.SpawnParticle(ParticleType.Blood, entity.Location);
            }
        }
    }
}
