using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{

    public class FareClassAttribute : BasicAttribute
    {
        [JsonProperty("journey_type")] 
        public string JourneyType { get; set; }

        [JsonProperty("iata_category")] 
        public string IataCategory { get; set; }
    }
}