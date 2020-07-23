using Ara3D;
using LiteNetLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SideWars.Shared.Game;
using SideWarsServer;
using SideWarsServer.Database.Models;
using SideWarsServer.Game;
using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;
using SideWarsServer.Networking;
using SideWarsServer.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SideWarsServerTests
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void HyrexBulettInherits()
        {
            var hb = new HyrexBullet(new Player(Vector3.Zero, PlayerInfo.Hyrex, null, SideWars.Shared.Packets.EntityTeam.Blue));
            Assert.IsTrue(hb is ITimedDestroy && hb is Bullet);
        }
    }
}
