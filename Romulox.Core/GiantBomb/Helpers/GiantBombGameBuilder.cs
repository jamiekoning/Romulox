using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Romulox.Core.Entities;
using Romulox.Core.GiantBomb.Api;
using Romulox.Core.GiantBomb.Entities;
using Romulox.Core.Helpers;
using Romulox.Core.NoIntro.Helpers;

namespace Romulox.Core.GiantBomb.Helpers
{
    public class GiantBombGameBuilder
    {
        private static string apiKey;
        private static DatFilesHelper datFilesHelper;
        private static ImageDownloader imageDownloader;

        private string path;
        private string name;
        private string transformedName;
        private string guid;
        private string image;
        private PlatformType platformType;
        private SearchResponse searchResponse;
        private GameResponse gameResponse;
        
        private GiantBombGameBuilder(Game game, PlatformType platformType)
        {
            this.platformType = platformType;
            path = game.Path;
                
            var rawGameName = datFilesHelper.FindGameName(game.Path);
                
            transformedName =
                rawGameName != null
                    ? DatFilesHelper.CleanName(rawGameName)
                    : DatFilesHelper.CleanName(game.Path);
        }

        public static void Initialize(string apiKey, DatFilesHelper datFilesHelper, ImageDownloader imageDownloader)
        {
            GiantBombGameBuilder.apiKey = apiKey;
            GiantBombGameBuilder.datFilesHelper = datFilesHelper;
            GiantBombGameBuilder.imageDownloader = imageDownloader;
        }
        
        public static async Task<List<Game>> BuildGamesAsync(Romulox.Core.Entities.Platform platform)
        {
            var unprocessedGames = platform.Games.Where(g => g.Processed == false);

            ICollection<GiantBombGameBuilder> gameBuilders = new List<GiantBombGameBuilder>();
            
            foreach (var game in unprocessedGames)
            {
                gameBuilders.Add(new GiantBombGameBuilder(game, platform.PlatformType));
            }
            
            try
            {
                await Task.WhenAll(gameBuilders.Select(b => b.SearchRequestAsync()));
            }
            catch (WebException)
            {
                // If we hit 401 then filter out any requests that were not able to be handled
                gameBuilders = gameBuilders.Where(b => b.searchResponse != null).ToList();
            }

            foreach (var gameBuilder in gameBuilders)
            {
                gameBuilder.FindGuidInSearchResponse();
            }
            
            try
            {
                await Task.WhenAll(gameBuilders.Select(b => b.GameRequestAsync()));
            }
            catch (WebException)
            {
                // If we hit 401 then filter out any requests that were not able to be handled
                gameBuilders = gameBuilders.Where(b => b.gameResponse != null).ToList();
            }
            
            // this *should* never 401 as it is not an API request
            await Task.WhenAll(gameBuilders.Select(b => b.DownloadImage()));

            var games = new List<Game>();
            
            foreach (var gameBuilder in gameBuilders)
            {
                games.Add(gameBuilder.BuildGame());
            }

            return games;
        }
        
        private async Task SearchRequestAsync()
        {
            GiantBombSearchRequester searchRequester = new GiantBombSearchRequester(apiKey);
            string gameNameWithoutPunctuation = transformedName.RemovePunctuation();
            
            searchRequester.SearchQuery = gameNameWithoutPunctuation;
            
            string response = await searchRequester.RequestAsync();
            searchResponse = JsonDependency.DeserializeWithSettings<SearchResponse>(response);
        }
        
        private async Task GameRequestAsync()
        {
            if (guid == null)
                return;
            
            GiantBombGameRequester gameRequester = new GiantBombGameRequester(apiKey);

            gameRequester.Guid = guid;
            
            string response = await gameRequester.RequestAsync();

            gameResponse = JsonDependency.DeserializeWithSettings<GameResponse>(response);
        }
        
        private void FindGuidInSearchResponse()
        {
            name = transformedName.RemovePunctuation();
            //string guid = null;

            foreach (var giantBombResult in searchResponse.Results)
            {
                if (guid != null)
                    break;
                
                if (giantBombResult.Platforms == null)
                    continue;

                foreach (var giantBombPlatform in giantBombResult.Platforms)
                {
                    if (giantBombPlatform.Abbreviation != platformType.ToString() || guid != null)
                        continue;
                    
                    string[] gameNameTokens = name.Split(' ');

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
        }

        private Game BuildGame()
        {
            Game game = new Game();
            game.Path = path;

            if (guid != null)
            {
                var result = gameResponse.Results;

                game.Name = result.Name;
                game.Description = result.Deck;

                if (result.Developers != null)
                    game.Developers = string.Join(", ", result.Developers.Select(d => d.Name));

                if (result.Publishers != null)
                    game.Publishers = string.Join(", ", result.Publishers.Select(p => p.Name));

                game.ReleaseDate = BuildReleaseDate();
                
                game.Image = image;
            }
            else
            {
                game.Name = name;
                game.Name = System.IO.Path.GetFileNameWithoutExtension(path);
                game.Description = "No Description Found";
            }

            // flag the game as processed if it has made it this far
            game.Processed = true;

            return game;
        }

        private DateTime BuildReleaseDate()
        {
            var result = gameResponse.Results;
            
            if (result.OriginalReleaseDate != null)
                return DateTime.Parse(result.OriginalReleaseDate);

            if (result.ExpectedReleaseYear != null && result.ExpectedReleaseMonth != null &&
                result.ExpectedReleaseDay != null)
                return new DateTime(result.ExpectedReleaseYear.Value, result.ExpectedReleaseMonth.Value,
                    result.ExpectedReleaseDay.Value);

            return DateTime.MinValue;
        }

        private async Task DownloadImage()
        {
            var imageUrl = gameResponse?.Results?.Image?.SmallUrl;

            if (imageUrl != null)
            {
                image = await imageDownloader.DownloadImageAsync(imageUrl, path);
            }
            else
            {
                image = null;
            }
            
           
        }
    }
}