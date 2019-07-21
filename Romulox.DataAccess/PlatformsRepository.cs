using System;
using System.Collections.Generic;
using System.Linq;
using Romulox.Core.Entities;

namespace Romulox.DataAccess
{
    public class PlatformsRepository : IPlatformsRepository
    {
        private RomuloxContext context;

        public PlatformsRepository(RomuloxContext context)
        {
            this.context = context;
        }
        
        public IEnumerable<Platform> GetPlatforms()
        {
            return context.Platforms.OrderBy(p => p.Name);
        }

        public Platform GetPlatform(Guid platformId)
        {
            return context.Platforms.FirstOrDefault(p => p.Id == platformId);
        }

        public Platform GetPlatform(PlatformType platformType)
        {
            return context.Platforms.FirstOrDefault(p => p.PlatformType == platformType);
        }

        public IEnumerable<Platform> GetPlatforms(IEnumerable<Guid> platformIds)
        {
            return context.Platforms.Where(p => platformIds.Contains(p.Id)).OrderBy(p => p.Name);
        }

        public void AddPlatform(Platform platform)
        {
            platform.Id = Guid.NewGuid();
            context.Add(platform);

            if (platform.Games.Any())
            {
                foreach (var game in platform.Games)
                {
                    game.Id = Guid.NewGuid();
                }
            }
        }

        public void DeletePlatform(Platform platform)
        {
            context.Platforms.Remove(platform);
        }

        public void UpdatePlatform(Platform platform)
        { }

        public bool PlatformExists(Guid platformId)
        {
            return context.Platforms.Any(p => p.Id == platformId);
        }

        public IEnumerable<Game> GetGamesForPlatform(Guid platformId)
        {
            return context.Games.Where(g => g.PlatformId == platformId).OrderBy(g => g.Name);
        }

        public Game GetGameForPlatform(Guid platformId, Guid gameId)
        {
            return context.Games.FirstOrDefault(g => g.PlatformId == platformId && g.Id == gameId);
        }

        public void AddGameForPlatform(Guid platformId, Game game)
        {
            var platform = GetPlatform(platformId);

            if (platform != null)
            {
                if (game.Id == Guid.Empty)
                {
                    game.Id = Guid.NewGuid();
                }

                platform.Games.Add(game);
            }
        }

        public void UpdateGameForPlatform(Game game)
        { }

        public void DeleteGame(Game game)
        {
            context.Games.Remove(game);
        }

        public bool Save()
        {
            return context.SaveChanges() >= 0;
        }
    }
}