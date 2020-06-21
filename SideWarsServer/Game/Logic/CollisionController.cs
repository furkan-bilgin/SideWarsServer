using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Models;
using System;
using System.Collections.Generic;

namespace SideWarsServer.Game.Logic
{
    public class CollisionController
    {
        Dictionary<ICollider, ICollider> collidedBodies;

        public CollisionController()
        {
            collidedBodies = new Dictionary<ICollider, ICollider>();
        }

        public void GetCollidingBodies(List<ICollider> colliders, Action<ICollider, CollisionData> onCollision)
        {
            collidedBodies.Clear();

            foreach (var collider in colliders)
            {
                foreach (var otherCollider in colliders)
                {
                    if (collider == otherCollider)
                        continue;

                    if (collidedBodies.ContainsKey(collider)) // Don't look the same colliders twice
                    {
                        var other = collidedBodies[collider];
                        if (other == otherCollider)
                            continue;
                    }
                    else if (collidedBodies.ContainsKey(otherCollider))
                    {
                        var current = collidedBodies[otherCollider];
                        if (collider == current)
                            continue;
                    }

                    if (collider.IsColliding(otherCollider)) // If we're colliding, fire the events
                    {
                        onCollision(collider, new CollisionData() { collider = otherCollider });
                        onCollision(otherCollider, new CollisionData() { collider = collider });

                        collidedBodies.Add(collider, otherCollider);
                        collidedBodies.Add(otherCollider, collider);
                    }
                }
            }
        }
    }
}
