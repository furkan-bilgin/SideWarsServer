namespace SideWars.Shared.Packets
{
    public class ServerPlayerMovementPacket
    {
        public int Id { get; set; }
        public sbyte Horizontal { get; set; }
        public bool Jump { get; set; }
    }
}
