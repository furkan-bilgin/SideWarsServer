using System;
using System.Collections.Generic;
using System.Text;

namespace SideWars.Shared.Packets
{
    public class EntityHaltPacket
    {
        public int Id { get; set; }
        public bool IsHalted { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
