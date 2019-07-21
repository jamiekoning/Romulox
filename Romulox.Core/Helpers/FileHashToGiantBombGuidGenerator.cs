using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Romulox.Core.Entities;
using Romulox.Core.GiantBomb;
using Romulox.Core.NoIntro.Entities;
using Romulox.Core.NoIntro.Helpers;

namespace Romulox.Core.Helpers
{
    public class FileHashToGiantBombGuidGenerator
    {
        private readonly NoIntroDatFile noIntroDatFile;
        private readonly PlatformType platformType;
        private readonly GiantBombGameProvider giantBombGameProvider;

        public FileHashToGiantBombGuidGenerator(string path, PlatformType platformType, string apiKey)
        {
            noIntroDatFile = new NoIntroDatFile().LoadFromFile(path);
            this.platformType = platformType;
            giantBombGameProvider = new GiantBombGameProvider(apiKey);
        }

        public void GenerateGuidsAsync(string region)
        {
            var gamesFromRegion = noIntroDatFile.NoIntroGames.Where(g =>
                g.Description.IndexOf("USA", StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
            
            Console.WriteLine(gamesFromRegion.Count);
            
            //var count = gamesFromRegion.Count(g => g.Description.IndexOf("Rev", StringComparison.CurrentCultureIgnoreCase) != -1);

            var batch = gamesFromRegion.GetRange(225, 100);
            
            batch = giantBombGameProvider.ProvideGamesAsync(batch, platformType).Result;
            
            var lines = new List<string>();
            foreach (var game in batch)
            {
                foreach (var rom in game.NoIntroRoms)
                {
                    string template = $"{game.Description}:{rom.Md5}:{game.GiantBombApiGuid}";
                    
                    if (game.GiantBombApiGuid == null)
                        template = $"{game.Description}:{rom.Md5}:NULL";
                    
                    lines.Add(template);
                }
            }

            File.AppendAllLines($"{platformType}_map.txt", lines);

        }
    }
}