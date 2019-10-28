using LaunchLocker.Interface;
using Newtonsoft.Json;

namespace LaunchLocker.Library
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object input) =>
            JsonConvert.SerializeObject(input, new JsonSerializerSettings() {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
    }
}
