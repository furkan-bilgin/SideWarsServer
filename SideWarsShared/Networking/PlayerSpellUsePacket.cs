namespace SideWars.Shared.Packets
{
    public class PlayerSpellUsePacket
    {
        public int Id { get; set; }
        public ushort SpellType { get; set; }
    }
}
