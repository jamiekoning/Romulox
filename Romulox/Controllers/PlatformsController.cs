using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Romulox.Core.Configuration.RomuloxSettings;
using Romulox.Core.Entities;
using Romulox.Core.GiantBomb;
using Romulox.Core.GiantBomb.Helpers;
using Romulox.Core.Helpers;
using Romulox.Core.Models;
using Romulox.Core.NoIntro.Helpers;
using Romulox.DataAccess;


namespace Romulox.Controllers
{
    [Route("api/platforms")]
    public class PlatformsController : Controller
    {
        private readonly IPlatformsRepository platformsRepository;
        private readonly IMapper mapper;
        private readonly IOptions<RomuloxSettings> romuloxSettings;
        private readonly IHostingEnvironment hostingEnvironment;
        
        public PlatformsController(IPlatformsRepository platformsRepository, IMapper mapper, 
            IOptions<RomuloxSettings> romuloxSettings, IHostingEnvironment hostingEnvironment)
        {
            this.platformsRepository = platformsRepository;
            this.mapper = mapper;
            this.romuloxSettings = romuloxSettings;
            this.hostingEnvironment = hostingEnvironment;
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
            
            foreach (var file in Directory.GetFiles(platformEntity.Path))
            {
                FileInfo fileInfo = new FileInfo(file);
                
                // skip empty and hidden
                if (fileInfo.Length == 0 || fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                    continue;

                Game game = new Game();
                game.Id = new Guid();
                game.Path = file;
                game.Name = Path.GetFileName(file);

                platformEntity.Games.Add(game);
            }

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
            platformFromRepository.Games = platformsRepository.GetGamesForPlatform(platformId).ToList();
            
            // add any new files in the directory
            foreach (var file in Directory.GetFiles(platformFromRepository.Path))
            {
                FileInfo fileInfo = new FileInfo(file);

                // Skip empty and hidden files
                if (fileInfo.Length == 0 || fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                    continue;

                if (platformFromRepository.Games.Count(g => g.Path == file) == 0)
                {
                    Game game = new Game();
                    game.Id = new Guid();
                    game.Path = file;
                    game.Name = Path.GetFileName(file);

                    platformFromRepository.Games.Add(game); 
                }
                
            }

            var datFilesService = 
                new DatFilesHelper(hostingEnvironment.WebRootPath + romuloxSettings.Value.Directories.DatFilesDirectory);
            
            var imageDownloader =
                new ImageDownloader(hostingEnvironment.WebRootPath, romuloxSettings.Value.Directories.ImagesDirectory);
            
            GiantBombGameBuilder.Initialize(romuloxSettings.Value.ApiKeys.GiantBombApiKey, datFilesService, imageDownloader);
            
            var processedGames = GiantBombGameBuilder.BuildGamesAsync(platformFromRepository).Result;

            var games = platformFromRepository.Games.ToList();
            foreach (var game in processedGames)
            {
                var index = games.FindIndex(g => g.Path == game.Path);

                if (index != -1)
                {
                    games[index] = game;
                }
            }

            platformFromRepository.Games = games;

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