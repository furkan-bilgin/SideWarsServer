using SideWars.Shared.Game;
using SideWars.Shared.Utils;
using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SideWarsServer.Game.Logic.Updater
{
    public class StatusEffectUpdater : IEntityUpdater
    {
        private Dictionary<int, int> latestEntityStatusEffects;

        public StatusEffectUpdater()
        {
            latestEntityStatusEffects = new Dictionary<int, int>();
        }

        public void Update(Entity entity, IGameRoom gameRoom)
        {
            if (entity.StatusEffects.Count <= 0)
                return;

            CheckExpiredStatusEffects(entity, gameRoom);

            var entityHash = entity.GetHashCode();
            var statusEffectHash = GetHashOfStatusEffects(entity.StatusEffects);

            if (!latestEntityStatusEffects.ContainsKey(entityHash))
            {
                UpdateStatusEffects(entity);
                latestEntityStatusEffects.Add(entityHash, statusEffectHash);
            }
            else if (latestEntityStatusEffects[entityHash] != statusEffectHash)
            {
                latestEntityStatusEffects[entityHash] = statusEffectHash;
                UpdateStatusEffects(entity);
            }
        }

        void CheckExpiredStatusEffects(Entity entity, IGameRoom gameRoom)
        {
            var willBeDeleted = new List<IStatusEffect>();
            foreach (var effect in entity.StatusEffects)
            {
                if (effect.SpawnTick + effect.ExpirityPeriod < gameRoom.Tick)
                {
                    willBeDeleted.Add(effect);   
                }
            }

            foreach (var effect in willBeDeleted)
            {
                entity.StatusEffects.Remove(effect);
            }
        }

        int GetHashOfStatusEffects(List<IStatusEffect> statusEffects)
        {
            var hash = 1;
            foreach (var item in statusEffects)
                hash *= item.GetHashCode();

            return hash;
        }

        void UpdateStatusEffects(Entity entity)
        {
            var info = (EntityInfo)entity.EntityInfo.Clone();
            foreach (var item in entity.StatusEffects)
            {
                info = item.ApplyEffect(info);
            }

            entity.UpdateEntityInfo(info);
        }
    }
}
