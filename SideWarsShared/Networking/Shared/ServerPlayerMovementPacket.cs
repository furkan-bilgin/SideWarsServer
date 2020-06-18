namespace SideWars.Shared.Packets
{
    public class ServerPlayerMovementPacket
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public byte Horizontal { get; set; }
        public byte Vertical { get; set; }
    }
}
