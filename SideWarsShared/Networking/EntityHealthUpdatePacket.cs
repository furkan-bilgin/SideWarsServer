namespace SideWars.Shared.Packets
{
    public class EntityHealthUpdatePacket
    {
        public int Id { get; set; }
        public ushort Health { get; set; }
    }
}
