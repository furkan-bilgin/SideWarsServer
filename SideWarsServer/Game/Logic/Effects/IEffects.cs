using SideWarsServer.Game.Room;

namespace SideWarsServer.Game.Logic.Effects
{
    public interface IEffect
    {
        void Start(IGameRoom room);
    }
}
