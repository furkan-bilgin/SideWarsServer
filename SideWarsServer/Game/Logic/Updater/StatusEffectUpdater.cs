using SideWars.Shared.Game;
using SideWars.Shared.Utils;
using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Logic.GameLoop;
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
            var entityInfoToBeSynced = new Dictionary<string, object>();

            // Save the status effect hash if it wasn't saved before
            if (!latestEntityStatusEffects.ContainsKey(entityHash))
            {
                entityInfoToBeSynced = UpdateStatusEffects(entity);
                latestEntityStatusEffects.Add(entityHash, statusEffectHash);
            }
            else if (latestEntityStatusEffects[entityHash] != statusEffectHash)
            {
                // Update status effects if the hash has changed
                latestEntityStatusEffects[entityHash] = statusEffectHash;
                entityInfoToBeSynced = UpdateStatusEffects(entity);
            }

            if (entityInfoToBeSynced.Count > 0)
                gameRoom.GetGameLoop<PacketSenderGameLoop>().OnEntityInfoChange(entity, entityInfoToBeSynced);
          
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
            var hash = 13;
            foreach (var item in statusEffects)
                hash = (hash * 7) + item.GetHashCode();

            return hash;
        }

        // List of EntityInfo fields we need to keep in sync with the client
        private Dictionary<string, Func<EntityInfo, object>> entityInfoSync = new Dictionary<string, Func<EntityInfo, object>>()
        {
            { "Speed", (x) => x.Speed },
            { "BaseHealth", (x) => x.BaseHealth }
        };

        Dictionary<string, object> UpdateStatusEffects(Entity entity)
        {
            var newInfo = (EntityInfo)entity.EntityInfo.Clone();
            var oldInfo = entity.EntityInfo;

            foreach (var item in entity.StatusEffects)
            {
                newInfo = item.ApplyEffect(newInfo);
            }

            // Sync important fields with client
            var entityInfoToBeSynced = new Dictionary<string, object>(); 
            foreach (var item in entityInfoSync)
            {
                var (name, checker) = (item.Key, item.Value);
                if (checker(newInfo) != checker(oldInfo))
                {
                    entityInfoToBeSynced.Add(name, checker(newInfo));
                }
            }

            entity.UpdateEntityInfo(newInfo);
            return entityInfoToBeSynced;
        }
    }
}
