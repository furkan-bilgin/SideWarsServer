namespace SideWars.Shared.Packets
{
    public class ParticleSpawnPacket
    {
        public ushort ParticleType { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float[] Data { get; set; }
    }
}
