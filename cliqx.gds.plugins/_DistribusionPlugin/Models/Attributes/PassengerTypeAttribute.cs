using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class PassengerTypeAttribute : BasicAttribute
    {
        [JsonProperty("min_Age")]
        public string MinAge { get; set; }
        [JsonProperty("max_Age")]
        public string MaxAge { get; set; }
    }
}
