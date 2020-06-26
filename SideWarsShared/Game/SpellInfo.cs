namespace SideWars.Shared.Game
{
    public struct SpellInfo
    {
        public int Cooldown { get; set; }
        public SpellType Type { get; set; }

        public SpellInfo(int cooldown, SpellType type)
        {
            Cooldown = cooldown;
            Type = type;
        }
    }
}
