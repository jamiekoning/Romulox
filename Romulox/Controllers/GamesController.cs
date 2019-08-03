using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Romulox.Core.Models;
using Romulox.DataAccess;

namespace Romulox.Controllers
{
    [Route("api/platforms/{platformId}/games")]
    public class GamesController : Controller
    {
        private readonly IPlatformsRepository platformsRepository;
        private readonly IMapper mapper;

        public GamesController(IPlatformsRepository platformsRepository, IMapper mapper)
        {
            this.platformsRepository = platformsRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGamesForPlatform(Guid platformId)
        {
            if (!platformsRepository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var gamesFromRepository = platformsRepository.GetGamesForPlatform(platformId);

            var games = mapper.Map<IEnumerable<GameDto>>(gamesFromRepository);

            return Ok(games);
        }

        [HttpGet("{gameId}")]
        public IActionResult GetGameForPlatform(Guid platformId, Guid gameId)
        {
            if (!platformsRepository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var gameFromRepository = platformsRepository.GetGameForPlatform(platformId, gameId);

            if (gameFromRepository == null)
            {
                return NotFound();
            }

            var game = mapper.Map<GameDto>(gameFromRepository);

            return Ok(game);
        }
        
        [HttpPost("{gameId}")]
        public IActionResult EditGameForPlatform(Guid platformId, Guid gameId, [FromBody] GameCreationDto game)
        {

            if (game == null)
            {
                return BadRequest();
            }
            
            var gameFromRepository = platformsRepository.GetGameForPlatform(platformId, gameId);

            if (gameFromRepository == null)
            {
                return NotFound();
            }

            mapper.Map(game, gameFromRepository);
            
            platformsRepository.UpdateGameForPlatform(gameFromRepository);

            if (!platformsRepository.Save())
            {
                return StatusCode(500, "There was an error handling your request");
            }

            return NoContent();
        }

        [HttpDelete("{gameId}")]
        public IActionResult DeleteGameForPlatform(Guid platformId, Guid gameId)
        {
            if (!platformsRepository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var gameFromRepository = platformsRepository.GetGameForPlatform(platformId, gameId);

            if (gameFromRepository == null)
            {
                return NotFound();
            }
            
            platformsRepository.DeleteGame(gameFromRepository);

            if (!platformsRepository.Save())
            {
                return StatusCode(500, "There was an error handling your request");
            }

            return NoContent();
        }
    }
}