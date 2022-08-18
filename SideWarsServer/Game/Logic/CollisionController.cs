using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Models;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;

namespace SideWarsServer.Game.Logic
{
    public class CollisionController
    {
        RandomStringGenerator stringGenerator = new RandomStringGenerator();

        HashSet<string> collidedBodies = new HashSet<string>();

        Dictionary<ICollider, Entity> collidingEntityDic = new Dictionary<ICollider, Entity>();
        List<ICollider> collidingEntityList = new List<ICollider>();
        Dictionary<ICollider, string> colliderHashes = new Dictionary<ICollider, string>();

        public void GetCollidingBodies(List<ICollider> colliders, Action<ICollider, CollisionData> onCollision)
        {
            collidedBodies.Clear();

            foreach (var collider in colliders)
            {
                foreach (var otherCollider in colliders)
                {
                    if (collider == otherCollider)
                        continue;

                    if (collidedBodies.Contains(colliderHashes[collider] + colliderHashes[otherCollider]))
                    {
                        continue;
                    }

                    if (collider.IsColliding(otherCollider)) // If we're colliding, fire the events
                    {
                        onCollision(collider, new CollisionData() { collider = otherCollider });
                        onCollision(otherCollider, new CollisionData() { collider = collider });

                        collidedBodies.Add(colliderHashes[collider] + colliderHashes[otherCollider]);
                        collidedBodies.Add(colliderHashes[otherCollider] + colliderHashes[collider]);
                    }
                }
            }
        }

        /// <summary>
        /// Gets colliding entities. 
        /// </summary>
        /// <param name="onCollision">First param is entity, second is colliding entity</param>
        public void GetCollidingEntities(List<Entity> entities, Action<Entity, Entity> onCollision)
        {
            collidingEntityDic.Clear();
            collidingEntityList.Clear();
            colliderHashes.Clear();

            foreach (var item in entities)
            {
                collidingEntityDic.Add(item.Collider, item);
                collidingEntityList.Add(item.Collider);
                colliderHashes.Add(item.Collider, stringGenerator.RandomString(15)); // Create random hashes for colliders
            }

            GetCollidingBodies(collidingEntityList, (collider, data) =>
            {
                onCollision(collidingEntityDic[collider], collidingEntityDic[data.collider]);
            });
        }
    }
}
