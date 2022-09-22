namespace SideWars.Shared.Packets
{
    public class RoundUpdatePacket
    {
        public int Round { get; set; }
        public int LastRoundWinner { get; set; }
        public bool GameFinished { get; set; }
    }
}
