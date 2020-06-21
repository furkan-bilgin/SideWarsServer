using LiteNetLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SideWarsServer;
using SideWarsServer.Database.Models;
using SideWarsServer.Game;
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
    public class RoomTests
    {
        [TestMethod]
        public void CreateRoomWorks()
        {
            var roomController = new RoomController();
            var server = new Server();
            server.LogicController = new LogicController(1);
            server.NetworkController = new NetworkController();

            var netManager = new NetManager(new EventBasedNetListener());
            netManager.Start();
            var netPeer = netManager.Connect("127.0.0.1", 9999, "");

            var player = new PlayerConnection(new Token(true, string.Empty, string.Empty), netPeer);

            Assert.IsTrue(roomController.JoinOrCreateRoom(player));
            netPeer.Disconnect();
        }
    }
}
