using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Abstract;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo repository;
        private readonly IMapper mapper;
        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            System.Console.WriteLine("--> Getting platforms...");
            var platformsList =  repository.GetAllPlatforms();
            return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformsList));
        }

        [HttpGet("{platformId}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int platformId)
        {
            var platform = repository.GetPlatformById(platformId);
            
            return (platform is not null) ? Ok(mapper.Map<PlatformReadDto>(platform)) : NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = mapper.Map<Platform>(platformCreateDto);
            repository.CreatePlatform(platformModel);
            repository.SaveChanges();

            var platformReadDto = mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(
                routeName: "GetPlatformById",
                routeValues: new { Id = platformReadDto.Id},
                value: platformReadDto
            );
        }
    }
}