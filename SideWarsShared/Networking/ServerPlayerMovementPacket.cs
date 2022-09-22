namespace SideWars.Shared.Packets
{
    public class ServerPlayerMovementPacket
    {
        public int[] Id { get; set; }
        public float[] Horizontal { get; set; }
        public float[] X { get; set; }
        public float[] Y { get; set; }
        public float[] Z { get; set; }
        public int Tick { get; set; }
        //public bool Jump { get; set; }
    }
}
