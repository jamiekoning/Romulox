using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Romulox.Core.Helpers
{
    public class JsonApiRequester
    {
        public string BaseUrl { get; set; }
        public string EndPoint { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; }
        
        public string TextResponse { get; set; }
        private string requestUrl;
        
        private WebClient webClient;
        public string UserAgentHeader { get; set; }
        private JsonDependency jsonDependency;

        public JsonApiRequester()
        {
            QueryParameters = new Dictionary<string, string>();
            jsonDependency = new JsonDependency();
        }

        private void BuildQueryString()
        {
            // Creates an '&' separated string of (Key=Value) pairs from the QueryParameters Dictionary
            var queryString = string.Join("&", QueryParameters.Select(q => q.Key + "=" + q.Value));

            requestUrl = $"{BaseUrl}/{EndPoint}?{queryString}";
        }
        
        public virtual void Request()
        {
            BuildQueryString();
            
            using (webClient = new WebClient())
            {
                if (UserAgentHeader != null)
                    webClient.Headers.Add("user-agent", UserAgentHeader);
                
                TextResponse = webClient.DownloadString(requestUrl);
            }
        }

        public virtual async Task<string> RequestAsync()
        {
            BuildQueryString();
            
            using (webClient = new WebClient())
            {
                if (UserAgentHeader != null)
                    webClient.Headers.Add("user-agent", UserAgentHeader);
                
                TextResponse = await webClient.DownloadStringTaskAsync(new Uri(requestUrl));
            }

            return TextResponse;
        }

        public T DeserializeJsonResponse<T>()
        {
            return jsonDependency.Deserialize<T>(TextResponse);
        }

        public T DeserializeJsonResponseWithSettings<T>()
        {
            return jsonDependency.DeserializeWithSettings<T>(TextResponse);
        }
    }
}