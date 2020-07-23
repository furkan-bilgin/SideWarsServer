using SideWarsServer.Game.Room;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public interface IGameLoop
    {
        void Update(IGameRoom gameRoom);
    }
}
