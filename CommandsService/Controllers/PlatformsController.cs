using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data.Abstract;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo repository;
        private readonly IMapper mapper;
        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;

        }

        /*                  MOVE ON HERE!!!!!                    */

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            System.Console.WriteLine("--> Getting platforms from CommandsService!");  
            var platformItems = repository.GetAllPlatforms();
            return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            System.Console.WriteLine("--> Inbound POST # Commands Service");
            return Ok("--> Inbound test of Platforms Controller");
        }
    }
}