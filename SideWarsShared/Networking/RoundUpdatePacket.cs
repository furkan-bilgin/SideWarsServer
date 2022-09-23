namespace SideWars.Shared.Packets
{
    public class RoundUpdatePacket
    {
        public int Round { get; set; }
        public EntityTeam LastRoundWinner { get; set; }
        public bool GameFinished { get; set; }
    }
}
