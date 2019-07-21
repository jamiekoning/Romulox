using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Romulox.Core.Helpers
{
    /* Wrapper for interacting with the Newtonsot JSON dependency */
    public class JsonDependency
    {    
        public readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        };

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public T DeserializeWithSettings<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JsonSerializerSettings);
        }
    }
}