namespace SideWars.Shared.Packets
{
    public class EntitySpawnPacket
    {
        public int Id { get; set; }
        /// <summary>
        /// EntityType casted as an int
        /// </summary>
        public int EntityType { get; set; }

        /// <summary>
        /// EntityData casted as an int
        /// </summary>
        public int[] Data { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
