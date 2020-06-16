namespace SideWars.Shared.Packets
{
    /// <summary>
    /// From Server to Client
    /// </summary>
    public class EntityMovementPacket
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
