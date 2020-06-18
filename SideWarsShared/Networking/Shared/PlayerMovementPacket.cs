namespace SideWars.Shared.Packets
{
    public class PlayerMovementPacket
    {
        public byte Horizontal { get; set; }
        public bool Jump { get; set; }
    }
}
