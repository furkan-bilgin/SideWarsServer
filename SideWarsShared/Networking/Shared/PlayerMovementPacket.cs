namespace SideWars.Shared.Packets
{
    public class PlayerMovementPacket
    {
        public sbyte Horizontal { get; set; }
        public bool Jump { get; set; }
    }
}
