using Ara3D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Logic.Projectiles;
using System;
using System.Collections.Generic;

namespace SideWarsServerTests
{
    [TestClass]
    public class PhysicsTests
    {
        [TestMethod]
        public void CollisionControllerWorks()
        {
            var collisionController = new CollisionController();
            var col1 = new SquareCollider(Vector3.Zero, -Vector3.One, Vector3.One);
            var col2 = new SquareCollider(Vector3.Zero, -Vector3.One, Vector3.One);

            var fireCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                collisionController.GetCollidingBodies(new List<ICollider>() { col1, col2 }, (victim, data) =>
                {
                    fireCount++;
                });
            }
           

            Assert.AreEqual(2000, fireCount);
        }

        [TestMethod]
        public void PlayerMoves()
        {
            var loc = Vector3.One;
            var playerMovement = new PlayerMovement(EntityTeam.Blue, new SquareCollider(Vector3.Zero, -Vector3.One, Vector3.One), 5);
            playerMovement.Update(1, ref loc);

            var currentLoc = loc.SetX(loc.X);

            playerMovement.Horizontal = 1;
            playerMovement.Update(1, ref loc);

            Assert.AreNotEqual(loc, currentLoc);
        }

        [TestMethod]
        public void BulletMoves()
        {
            var loc = Vector3.One;
            var prev = Vector3.One;
            var bulletMovement = new BulletMovement(EntityTeam.Blue, 5);

            bulletMovement.Update(1, ref loc);

            Assert.AreNotEqual(loc, prev);
        }

        [TestMethod]
        public void HyrexBulletMoves()
        {
            var loc = Vector3.One;
            var prev = Vector3.One;
            var bulletMovement = new HyrexBulletMovement(EntityTeam.Blue, 5, 1);

            bulletMovement.Update(1, ref loc);

            Assert.AreNotEqual(loc, prev);
        }

        [TestMethod]
        public void HyrexBulletCollides()
        {
            var hb = new HyrexBullet(new Player(Vector3.Zero, PlayerInfo.Hyrex, null, SideWars.Shared.Packets.EntityTeam.Blue));
            hb.Collider.UpdateLocation(Vector3.Zero);
            Assert.IsTrue(hb.Collider.IsColliding(new SquareCollider(Vector3.Zero, -Vector3.One, Vector3.One)));
        }
    }
}
