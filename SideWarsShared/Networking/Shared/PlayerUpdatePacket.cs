namespace SideWars.Shared.Packets
{
    public enum PlayerButton
    {
        Fire,
        Special1,
        Special2
    }

    public class PlayerUpdatePacket
    {
        public byte[] PlayerButtons { get; set; }
        public sbyte Horizontal { get; set; }
        public bool Jump { get; set; }
    }
}
