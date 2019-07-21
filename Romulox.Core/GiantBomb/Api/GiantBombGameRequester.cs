using Romulox.Core.GiantBomb.Entities;

namespace Romulox.Core.GiantBomb.Api
{
    public class GiantBombGameRequester : GiantBombApiRequester<GameResponse>
    {
        public string Guid
        {
            // The EndPoint will be in the format "game/<Guid>"
            // Therefore we take substring after '/'
            get => base.EndPoint.Substring(base.EndPoint.IndexOf('/') + 1);

            set => base.EndPoint = $"game/{value}";
        }
        
        public GiantBombGameRequester(string apiKey) : base(apiKey)
        {
            base.QueryParameters["field_list"] = "deck,developers,name,original_release_date,image,publishers," +
                                                 "expected_release_year,expected_release_month,expected_release_day";
            
            base.QueryParameters["format"] = "json";
            base.EndPoint = "game";
        }
    }
}