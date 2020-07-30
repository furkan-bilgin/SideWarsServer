using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Models;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;

namespace SideWarsServer.Game.Logic
{
    public class CollisionController
    {
        HashSet<int> collidedBodies = new HashSet<int>();
        Dictionary<ICollider, Entity> collidingEntityDic = new Dictionary<ICollider, Entity>();
        List<ICollider> collidingEntityList = new List<ICollider>();

        public void GetCollidingBodies(List<ICollider> colliders, Action<ICollider, CollisionData> onCollision)
        {
            collidedBodies.Clear();

            foreach (var collider in colliders)
            {
                foreach (var otherCollider in colliders)
                {
                    if (collider == otherCollider)
                        continue;

                    if (collidedBodies.Contains(collider.GetHashCode() * otherCollider.GetHashCode()))
                    {
                        continue;
                    }

                    if (collider.IsColliding(otherCollider)) // If we're colliding, fire the events
                    {
                        onCollision(collider, new CollisionData() { collider = otherCollider });
                        onCollision(otherCollider, new CollisionData() { collider = collider });

                        collidedBodies.Add(collider.GetHashCode() * otherCollider.GetHashCode());
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

            foreach (var item in entities)
            {
                collidingEntityDic.Add(item.Collider, item);
                collidingEntityList.Add(item.Collider);
            }

            GetCollidingBodies(collidingEntityList, (collider, data) =>
            {
                onCollision(collidingEntityDic[collider], collidingEntityDic[data.collider]);
            });
        }
    }
}
