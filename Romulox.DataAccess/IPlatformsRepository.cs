using System;
using System.Collections.Generic;
using Romulox.Core.Entities;

namespace Romulox.DataAccess
{    
    public interface IPlatformsRepository
    {   
        IEnumerable<Platform> GetPlatforms();
        Platform GetPlatform(Guid platformId);
        Platform GetPlatform(PlatformType platformType);
        IEnumerable<Platform> GetPlatforms(IEnumerable<Guid> platformIds);

        void AddPlatform(Platform platform);
        void DeletePlatform(Platform platform);
        void UpdatePlatform(Platform platform);
        bool PlatformExists(Guid platformId);

        IEnumerable<Game> GetGamesForPlatform(Guid platformId);
        Game GetGameForPlatform(Guid platformId, Guid gameId);
        void AddGameForPlatform(Guid platformId, Game game);
        void UpdateGameForPlatform(Game game);
        void DeleteGame(Game game);

        bool Save();
    }
}