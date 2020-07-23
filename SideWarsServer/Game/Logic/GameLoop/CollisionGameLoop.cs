using SideWarsServer.Game.Room;
using System;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public class CollisionGameLoop : IGameLoop
    {
        private CollisionController collisionController;
        private IGameRoom gameRoom;
        private Action<Entity, Entity> callback;

        public CollisionGameLoop(Action<Entity, Entity> callback)
        {
            this.callback = callback;
            collisionController = new CollisionController();
        }

        public void Update(IGameRoom gameRoom)
        {
            this.gameRoom = gameRoom;
            UpdateColliders();
            UpdateCollisions();
        }

        private void UpdateColliders()
        {
            foreach (var item in gameRoom.GetEntities())
            {
                item.Collider.UpdateLocation(item.Location);
            }
        }

        private void UpdateCollisions()
        {
            collisionController.GetCollidingEntities(gameRoom.GetEntities(), callback);
        }
    }
}
