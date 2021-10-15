using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Data.Abstract;
using CommandsService.Models;

namespace CommandsService.Data.Concrete
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext context;
        public CommandRepo(AppDbContext context)
        {
            this.context = context;

        }

        public void CreateCommand(int platformId, Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
                command.PlatformId = platformId;
                context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            } 
            context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return context.Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
            return context.Platforms.Any(p=>p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() >= 0;
        }
    }
}