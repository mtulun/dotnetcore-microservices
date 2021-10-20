using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data.Abstract;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo repository;
        private readonly IMapper mapper;
        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatorm(int platformId)
        {
            System.Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            var commands = repository.GetCommandsForPlatform(platformId);
            
            return (!repository.PlatformExists(platformId)) ? NotFound() : Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        // Action Result level route.
        [HttpGet("{commandId}", Name ="GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            System.Console.WriteLine($"--> Hittin GetCommandForPlatform: {platformId} / {commandId}");

            if (!repository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = repository.GetCommand(platformId,commandId);

            return (command is null) ? NotFound() : Ok(mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            System.Console.WriteLine($"--> Hitting CreateCommandForPlatform: {platformId}");

            if (!repository.PlatformExists(platformId))
            {
                return NotFound();
            }
            var command = mapper.Map<Command>(commandCreateDto);

            repository.CreateCommand(platformId,command);
            repository.SaveChanges();

            // Model created and we get the new data after the savechanges().
            var commandReadDto = mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform), new {platformId = platformId,commandId = commandReadDto.Id}, commandReadDto);
        }
    }
}