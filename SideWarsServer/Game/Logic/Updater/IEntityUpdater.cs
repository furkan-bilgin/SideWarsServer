using SideWarsServer.Game.Room;

namespace SideWarsServer.Game.Logic.Updater
{
    public interface IEntityUpdater
    {
        void Update(Entity entity, IGameRoom gameRoom);
    }
}
