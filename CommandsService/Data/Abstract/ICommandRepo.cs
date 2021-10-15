using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data.Abstract
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        // Platform related stuffs
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);

        // Commands
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId,Command command);
    }
}