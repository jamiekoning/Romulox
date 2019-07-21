using Romulox.Core.Helpers;

namespace Romulox.Core.GiantBomb.Api
{
    public class GiantBombApiRequester<TResponse> : JsonApiRequester
    {
        public TResponse DeserializedResponse { get; set; }
        
        
        public GiantBombApiRequester(string apiKey) : base()
        {
            QueryParameters["api_key"] = apiKey;
            base.BaseUrl = "https://www.giantbomb.com/api";
            base.UserAgentHeader = "Romulox Comes In Peace";
        }
        
        public override void Request()
        {
            base.Request();
            DeserializedResponse = base.DeserializeJsonResponseWithSettings<TResponse>();
        }
        
    }
}