namespace SideWars.Shared.Packets
{
    public class EntitySpawnPacket
    {
        public int Id { get; set; }

        /// <summary>
        /// EntityType casted as a byte
        /// </summary> 
        public byte EntityType { get; set; }

        /// <summary>
        /// EntityData casted as a byte
        /// </summary>
        public ushort[] Data { get; set; }

        /// <summary>
        /// Big datas such as floats etc. Like it's bigger than ushort, right?
        /// </summary>
        public float[] BigData { get; set; }

        /// <summary>
        /// EntityTeam casted as a byte
        /// </summary>
        public byte Team { get; set; }
        
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public ushort Health { get; set; }
    }
}
