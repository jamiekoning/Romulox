using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Romulox.Core.Entities;
using Romulox.Core.Interfaces;
using Romulox.Core.Models;
using Romulox.Core.NoIntro.Transformers;
using Romulox.DataAccess;

namespace Romulox.Controllers
{
    [Route("api/platforms")]
    public class PlatformsController : Controller
    {
        private readonly IPlatformsRepository platformsRepository;
        private readonly IGameProvider giantBombGameProvider;
        private readonly IMapper mapper;
        
        public PlatformsController(IPlatformsRepository platformsRepository, IGameProvider giantBombGameProvider, IMapper mapper)
        {
            this.platformsRepository = platformsRepository;
            this.giantBombGameProvider = giantBombGameProvider;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetPlatforms()
        {
            var platformsFromRepository = platformsRepository.GetPlatforms();

            var platforms = mapper.Map<IEnumerable<PlatformDto>>(platformsFromRepository);

            return Ok(platforms);
        }

        [HttpGet("{platformId}", Name = "GetPlatform")]
        public IActionResult GetPlatform(Guid platformId)
        {
            var platformFromRepository = platformsRepository.GetPlatform(platformId);

            if (platformFromRepository == null)
            {
                return NotFound();
            }

            var platform = mapper.Map<PlatformDto>(platformFromRepository);

            return Ok(platform);
        }

        [HttpPost]
        public IActionResult CreatePlatform([FromBody] PlatformCreationDto platform)
        {
            if (platform == null)
            {
                return BadRequest();
            }

            var platformEntity = mapper.Map<Platform>(platform);
            /*
            platformEntity.Games = giantBombGameProvider.ProvideGamesAsync(
                platformEntity.Path,
                platformEntity.PlatformType,
                platformEntity.Games.ToList(),
                new NoIntroMetaTransformer(platformEntity.NoIntroDatFilePath)
            ).Result;*/
            
            platformsRepository.AddPlatform(platformEntity);

            if (!platformsRepository.Save())
            {
                return StatusCode(500, "There was a problem handling your request.");
            }

            RefreshGamesForPlatform(platformEntity.Id);

            var platformDto = mapper.Map<PlatformDto>(platformEntity);

            return CreatedAtRoute("GetPlatform", 
                new { platformId = platformDto.Id },
                platformDto);
        }

        [Route("types")]
        [HttpGet]
        public IActionResult GetPlatformTypes()
        {
            var platformTypeNames = Enum.GetNames(typeof(PlatformType));

            return Ok(platformTypeNames);
        }
        
        [Route("{platformId}/refresh")]
        [HttpGet]
        public IActionResult RefreshGamesForPlatform(Guid platformId)
        {
            if (!platformsRepository.PlatformExists(platformId))
            {
                return NotFound();
            }
            
            var platformFromRepository = platformsRepository.GetPlatform(platformId);

            var currentGames = platformsRepository.GetGamesForPlatform(platformId).ToList();

            var newGames = giantBombGameProvider.ProvideGamesAsync(
                platformFromRepository,
                new NoIntroMetaTransformer(platformFromRepository.NoIntroDatFilePath)
            ).Result;

            foreach (var game in newGames)
            {
                platformFromRepository.Games.Add(game);
            }

            if (!platformsRepository.Save())
            {
                return StatusCode(500, "There was an error handling your request");
            }

            return NoContent();
        }

        [HttpDelete("{platformId}")]
        public IActionResult DeletePlatform(Guid platformId)
        {
            if (!platformsRepository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var platformFromRepository = platformsRepository.GetPlatform(platformId);

            if (platformFromRepository == null)
            {
                return NotFound();
            }
            
            platformsRepository.DeletePlatform(platformFromRepository);

            if (!platformsRepository.Save())
            {
                return StatusCode(500, "There was an error handling your request");
            }

            return NoContent();
        }
    }
}