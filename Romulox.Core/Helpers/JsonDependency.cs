using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Romulox.Core.Helpers
{
    /* Wrapper for interacting with the Newtonsot JSON dependency */
    public class JsonDependency
    {    
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        };

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public static T DeserializeWithSettings<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JsonSerializerSettings);
        }
    }
}