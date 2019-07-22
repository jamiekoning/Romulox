using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Romulox.Core.Entities;
using Romulox.Core.GiantBomb.Api;
using Romulox.Core.GiantBomb.Entities;
using Romulox.Core.Helpers;
using Romulox.Core.Interfaces;
using Romulox.Core.NoIntro.Entities;
using Romulox.Core.NoIntro.Transformers;
using Platform = Romulox.Core.Entities.Platform;

namespace Romulox.Core.GiantBomb
{
    public struct GameInfo
    {
        public string path;
        public string directory;
        public string gameName;
        public string transformedName;
        public string guid;
        public string iconImage;
        public string image;
        public PlatformType platformType;
        public SearchResponse searchResponse;
        public GameResponse gameResponse;
    }
    
    public class GiantBombGameProvider : IGameProvider
    {
        private PathTools pathTools;
        private string apiKey;

        private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        };

        public GiantBombGameProvider(string apiKey)
        {
            pathTools = new PathTools();
            this.apiKey = apiKey;
        }

        public async Task<List<Game>> ProvideGamesAsync(Romulox.Core.Entities.Platform platform)
        {
            var path = platform.Path;
            var platformType = platform.PlatformType;
            var unprocessedGames = platform.Games.Where(g => g.Processed == false).ToList();
            var gameNameTransformer = new NoIntroMetaTransformer(platform.NoIntroDatFilePath);

            List<Game> games = new List<Game>();
            List<GameInfo> gameInfos = new List<GameInfo>();

            foreach (var game in unprocessedGames)
            {
                if (game.Path != null)
                {
                    GameInfo gameInfo = new GameInfo();
                    gameInfo.directory = path;
                    gameInfo.platformType = platformType;
                    gameInfo.path = game.Path;
                    gameInfo.transformedName = gameNameTransformer.Transform(game.Path) ??
                                      Path.GetFileNameWithoutExtension(game.Path);
                    
                    

                    gameInfos.Add(gameInfo);
                }
            }

            try
            {
                gameInfos = (await Task.WhenAll(gameInfos.Select(SearchRequestAsync))).ToList();
            }
            catch (WebException)
            {
                // If we hit 401 then filter out any requests that were not able to be handled
                gameInfos = gameInfos.Where(g => g.searchResponse != null).ToList();
            }

            for (int i = 0; i < gameInfos.Count; i++)
            {
                GameInfo gameInfo = gameInfos[i];
                gameInfo.guid = FindGuidInSearchResponse(gameInfo);
                gameInfos[i] = gameInfo;
            }

            try
            {
                gameInfos = (await Task.WhenAll(gameInfos.Select(GameRequestAsync))).ToList();
            }
            catch (WebException)
            {
                // If we hit 401 then filter out any requests that were not able to be handled
                gameInfos = gameInfos.Where(g => g.gameResponse != null).ToList();
            }

            // this should never 401 as it is not an API request
            gameInfos = (await Task.WhenAll(gameInfos.Select(SaveImagesAsync))).ToList();

            // finish the rest of the processing as we have the responses that we are interested in
            foreach (GameInfo gameInfo in gameInfos)
            {
                Game game = new Game();
                game.Path = gameInfo.path;
                
                if (gameInfo.guid != null)
                {
                    var result = gameInfo.gameResponse.Results;
                    
                    game.Name = result.Name;
                    game.Description = result.Deck;
                    
                    if (result.Developers != null)
                        game.Developers = string.Join(", ", result.Developers.Select(d => d.Name));
                
                    if (result.Publishers != null)
                        game.Publishers = string.Join(", ", result.Publishers.Select(p => p.Name));

                    game.ReleaseDate = BuildReleaseDate(result);

                    game.IconImage = gameInfo.iconImage;
                    game.Image = gameInfo.image;
                }
                else
                {
                    game.Name = gameInfo.gameName;
                    game.Name = Path.GetFileNameWithoutExtension(gameInfo.path);
                    game.Description = "No Description Found";
                }

                // flag the game as processed if it has made it this far
                game.Processed = true;
                
                games.Add(game);
                
            }

            return games;
        }

        private async Task<GameInfo> SearchRequestAsync(GameInfo gameInfo)
        {
            GiantBombSearchRequester searchRequester = new GiantBombSearchRequester(apiKey);
            string gameNameWithoutPunctuation = gameInfo.transformedName.RemovePunctuation();
            
            searchRequester.SearchQuery = gameNameWithoutPunctuation;
            
            string response = await searchRequester.RequestAsync();
            gameInfo.searchResponse = JsonConvert.DeserializeObject<SearchResponse>(response, jsonSerializerSettings);

            return gameInfo;
        }

        private async Task<GameInfo> GameRequestAsync(GameInfo gameInfo)
        {
            if (gameInfo.guid == null)
                return gameInfo;
            
            GiantBombGameRequester gameRequester = new GiantBombGameRequester(apiKey);

            gameRequester.Guid = gameInfo.guid;
            
            string response = await gameRequester.RequestAsync();

            gameInfo.gameResponse = JsonConvert.DeserializeObject<GameResponse>(response, jsonSerializerSettings);

            return gameInfo;
        }
        
        private async Task<GameInfo> SaveImagesAsync(GameInfo gameInfo)
        {
            if (gameInfo.guid == null)
                return gameInfo;
            
            DirectoryInfo imageDirectory = Directory.CreateDirectory(gameInfo.directory + "/images");
            string iconImagePath = $"{imageDirectory.FullName}/{gameInfo.transformedName}_IconImage.jpg";
            string imagePath = $"{imageDirectory.FullName}/{gameInfo.transformedName}_Image.jpg";
            
            // We have to use two webclients in succession otherwise the GiantBomb API will return 403
            // on the second DownloadFile() call
            Task iconTask;
            using (var webClient = new WebClient())    
            {
                webClient.Headers.Add("user-agent", "Romulox Comes In Peace.");

                iconTask = webClient.DownloadFileTaskAsync(gameInfo.gameResponse.Results.Image.IconUrl, iconImagePath);
            }

            Task imageTask;
            using (var wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Romulox Comes In Peace.");
                
                imageTask = wc.DownloadFileTaskAsync(gameInfo.gameResponse.Results.Image.SmallUrl, imagePath);
            }

            await iconTask;
            await imageTask;
            
            gameInfo.iconImage = pathTools.AbsolutePathToWebRootPath(iconImagePath);
            gameInfo.image = pathTools.AbsolutePathToWebRootPath(imagePath);

            return gameInfo;
        }

        private string FindGuidInSearchResponse(GameInfo gameInfo)
        {
            gameInfo.gameName = gameInfo.transformedName.RemovePunctuation();
            string guid = null;

            foreach (var giantBombResult in gameInfo.searchResponse.Results)
            {
                if (guid != null)
                    break;
                
                if (giantBombResult.Platforms == null)
                    continue;

                foreach (var giantBombPlatform in giantBombResult.Platforms)
                {
                    if (giantBombPlatform.Abbreviation != gameInfo.platformType.ToString() || guid != null)
                        continue;
                    
                    string[] gameNameTokens = gameInfo.gameName.Split(' ');

                    bool resultNameContainsTokens =
                        giantBombResult.Name.RemovePunctuation().ContainsTokens(gameNameTokens);

                    bool resultAliasContainsTokens =
                        giantBombResult.Aliases.RemovePunctuation().ContainsTokens(gameNameTokens);
                    
                    if (resultNameContainsTokens || resultAliasContainsTokens)
                    {
                        guid = giantBombResult.Guid;
                    }
                }
            }

            return guid;
        }

        private DateTime BuildReleaseDate(Results result)
        {
            if (result.OriginalReleaseDate != null)
                return DateTime.Parse(result.OriginalReleaseDate);

            if (result.ExpectedReleaseYear != null && result.ExpectedReleaseMonth != null &&
                result.ExpectedReleaseDay != null)
                return new DateTime(result.ExpectedReleaseYear.Value, result.ExpectedReleaseMonth.Value,
                    result.ExpectedReleaseDay.Value);

            return DateTime.MinValue;
        }
    }
}