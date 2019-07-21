using Romulox.Core.GiantBomb.Entities;

namespace Romulox.Core.GiantBomb.Api
{
    public class GiantBombSearchRequester : GiantBombApiRequester<SearchResponse>
    {
        public string SearchQuery
        {
            get => base.QueryParameters["query"];

            set => base.QueryParameters["query"] = value;
        }
        
        public GiantBombSearchRequester(string apiKey) : base(apiKey)
        {
            base.QueryParameters["resources"] = "game";
            base.QueryParameters["format"] = "json";
            base.EndPoint = "search";
        }
        
    }
}